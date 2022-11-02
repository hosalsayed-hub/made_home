using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel;
using Homemade.BLL.ViewModel.Employees;
using Homemade.DAL.Contract;
using Homemade.Model;
using Homemade.Model.Emp;
using Microsoft.AspNetCore.Identity;

namespace Homemade.BLL.Emp
{
    public class BlEmployee
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        private readonly BLPermission _bLPermission;
        private readonly UserManager<User> _userManager;
        public BlEmployee(BLPermission BLPermission, UserManager<User> userManager, IUOW _uow, BLGeneral blGeneral)
        {
            _userManager = userManager;
            _blGeneral = blGeneral;
            this._bLPermission = BLPermission;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Employees Name Is Used Before Or Not اختبار هل اسم موظف عربي موجود من قبل ام لا
        public bool IsExistMobile(string MobileNo, int Id) =>
            Uow.Employees.GetAll().Any(query => query.EntityEmpID != Id && query.MobileNo.Trim() == MobileNo.Trim() && !query.IsDeleted);
        public bool IsExistPhone(string PhoneNumber, int Id) =>
            Uow.Employees.GetAll().Any(query => query.EntityEmpID != Id && query.PhoneNumber.Trim() == PhoneNumber.Trim() && !query.IsDeleted);
        public bool IsExistEmail(string Email, int Id) =>
            Uow.Employees.GetAll().Any(query => query.EntityEmpID != Id && query.Email.Trim() == Email.Trim() && !query.IsDeleted);
        public bool IsExistIdNo(string IdNo, int Id) =>
           Uow.Employees.GetAll().Any(query => query.EntityEmpID != Id && query.IDNo.Trim() == IdNo.Trim() && !query.IsDeleted);
        public bool IsExistFileNo(string FileNo, int Id) =>
          Uow.Employees.GetAll().Any(query => query.EntityEmpID != Id && query.FileNo.Trim() == FileNo.Trim() && !query.IsDeleted);
        #endregion
        #region Actions
        /// Add New Employees اضافة موظف
        public async Task<bool> Insert(EmployeesViewModel EmployeesModel)
        {
            try
            {
                var Emp = new Employees
                {
                    FirstNameAR = EmployeesModel.FirstNameAR,
                    FirstNameEN = EmployeesModel.FirstNameEN,
                    CreateDate = _blGeneral.GetDateTimeNow(),
                    UserId = EmployeesModel.UserId,
                    IsEnable = true,
                    EnableDate = _blGeneral.GetDateTimeNow(),
                    IsDeleted = false,
                    AddressAR = EmployeesModel.AddressAR,
                    AddressEN = EmployeesModel.AddressEN,
                    CityID = EmployeesModel.CityID,
                    EntityEmpGuid = Guid.NewGuid(),
                    NationalityID = EmployeesModel.NationalityID,
                    Lat = EmployeesModel.Lat,
                    Photo = EmployeesModel.Photo,
                    Lng = EmployeesModel.Lng,
                    Zoom = EmployeesModel.Zoom,
                    UserEnable = EmployeesModel.UserId,
                    PhoneNumber = EmployeesModel.PhoneNumber,
                    Email = EmployeesModel.Email,
                    MobileNo = EmployeesModel.MobileNo,
                    Notes = EmployeesModel.Notes,
                    BirthDateHijri = EmployeesModel.BirthDateHijri,
                    BirthDateMilady = EmployeesModel.BirthDateMilady,
                    BloodTypeId = EmployeesModel.BloodTypeId,
                    FileNo = EmployeesModel.FileNo,
                    IDNo = EmployeesModel.IDNo,
                    Gender = EmployeesModel.Gender,
                    LastNameAR = string.Empty,
                    LastNameEN = string.Empty,
                    MidNameAR = string.Empty,
                    MidNameEN = string.Empty,
                    JobsID = EmployeesModel.JobsID,
                    //DepartmentsID = EmployeesModel.DepartmentsID,
                };
                Emp = Uow.Employees.Insert(Emp);
                string[] rolesstr = EmployeesModel.Roles;
                foreach (var item in rolesstr)
                {
                    if (EmployeesModel.UserId != 0 && !string.IsNullOrWhiteSpace(item))
                    {
                        var RoleID = int.Parse(item);
                        var user = await _userManager.FindByIdAsync(EmployeesModel.UserId.ToString());
                        if (!_bLPermission.GetIsUserInRole(RoleID, EmployeesModel.UserId))
                        {
                            var identityResult = _bLPermission.CreateUserRole(RoleID, EmployeesModel.UserId);
                        }
                        var Model = new PermissionViewModel();
                        Model.UserId = EmployeesModel.UserId;
                        Model.RoleID = RoleID;
                        Model.CheckedItems = EmployeesModel.CheckedItems;
                        var Res = _bLPermission.EditRolePermissionsNoCommit(Model);
                    }
                }
                Uow.Commit();
            }
            catch (Exception ex)
            {

                throw;
            }
            return true;
        }
        /// Delete Employees By ID حذف موظف
        public bool Delete(int id, int userId)
        {
            var Employees = GetById(id);
            if (Employees != null)
            {
                Employees.IsDeleted = true;
                Employees.UserDelete = userId;
                Employees.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Employees.Update(Employees);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = Uow.Employees.GetAll(p => p.EntityEmpID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Employees.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Employees تعديل موظف
        public bool Update(EmployeesViewModel model, string MainPath, string MainPathView)
        {
            var obj = GetById(model.EntityEmpID);
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(model.Photo))
                {
                    if (MainPathView + obj.Photo != model.Photo)
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        var Photo = FileName;
                        _blGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.Photo,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        obj.Photo = Photo;
                    }
                }


                obj.UserUpdate = model.UserUpdate;
                obj.UpdateDate = _blGeneral.GetDateTimeNow();
                obj.FirstNameAR = model.FirstNameAR;
                obj.FirstNameEN = model.FirstNameEN;
                obj.AddressAR = model.AddressAR;
                obj.AddressEN = model.AddressEN;
                obj.CityID = model.CityID;
                obj.NationalityID = model.NationalityID;
                obj.Lat = model.Lat;
                //obj.Photo = model.Photo;
                obj.Lng = model.Lng;
                obj.Zoom = model.Zoom;
                obj.UserEnable = model.UserId;
                obj.PhoneNumber = model.PhoneNumber;
                obj.Email = model.Email;
                obj.MobileNo = model.MobileNo;
                obj.Notes = model.Notes;
                obj.BirthDateHijri = model.BirthDateHijri;
                obj.BirthDateMilady = model.BirthDateMilady;
                obj.BloodTypeId = model.BloodTypeId;
                obj.FileNo = model.FileNo;
                obj.IDNo = model.IDNo;
                obj.Gender = model.Gender;
                //obj.LastNameAR = model.LastNameAR;
                //obj.LastNameEN = model.LastNameEN;
                //obj.MidNameAR = model.MidNameAR;
                //obj.MidNameEN = model.MidNameEN;
                 obj.JobsID = model.JobsID;
                //obj.DepartmentsID = model.DepartmentsID;
                obj = Uow.Employees.Update(obj);
                string[] rolesstr = model.Roles;
                foreach (var item in rolesstr)
                {
                    if (obj.UserId != 0 && !string.IsNullOrWhiteSpace(item))
                    {
                        var RoleID = int.Parse(item);
                       
                        var Model = new PermissionViewModel();
                        Model.UserId = obj.UserId;
                        Model.RoleID = RoleID;
                        Model.CheckedItems = model.CheckedItems;
                        var Res = _bLPermission.EditRolePermissionsNoCommit(Model);
                    }
                }
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Update(int EntityEmpID, ProfileViewModel model)
        {
            var obj = GetById(EntityEmpID);
            if (obj != null)
            {
             
                obj.UserUpdate = model.UserUpdate;
                obj.UpdateDate = _blGeneral.GetDateTimeNow();
                obj.FirstNameAR = model.FirstNameAR;
                obj.FirstNameEN = model.FirstNameEN;
                obj.Email = model.Email;
                obj.MobileNo = model.MobileNo;
                obj.LastNameEN = model.LastNameEN;
                obj.LastNameAR = model.LastNameAR;
                obj = Uow.Employees.Update(obj);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdatePersonalImage(int OperatorId, string domain, string imgPath, ref string FileName, Stream stream, int userId)
        {
            var op = GetById(OperatorId);
            if (op != null)
            {
                op.Photo = /*domain + imgPath +*/ FileName;
                string MainPath = imgPath;

                _blGeneral.SaveImageWithStream(new BLGeneral.SaveImageModelStream
                {
                    Stream = stream,
                    key = "",
                    fileName = FileName,
                    mainPath = MainPath
                });

                FileName = op.Photo;
                op.UpdateDate = _blGeneral.GetDateTimeNow();
                op.UserUpdate = userId;
                op = Uow.Employees.Update(op);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public Employees GetById(int id) => Uow.Employees.GetAll(x => x.EntityEmpID == id).FirstOrDefault();
        #endregion
        #region GetEmployees
        public Employees GetByMobile(string mobile)
        {
            var data = Uow.Employees.GetAll(x => !x.IsDeleted && x.MobileNo == mobile, "City.Region").FirstOrDefault();
            return data;
        }
        public Employees GetByMobile(string mobile, string include)
        {
            var data = Uow.Employees.GetAll(x => !x.IsDeleted && x.MobileNo == mobile, include).FirstOrDefault();
            return data;
        }
        public ProfileViewModel GetProfileViewModelByUser(int UserID, string lang)
        {
           var data =  Uow.Employees.GetAll(x => !x.IsDeleted && x.UserId == UserID).Select(p => new ProfileViewModel()
            {
                MobileNo = p.MobileNo,
                Email = p.Email,
                 FirstNameAR = p.FirstNameAR,
                FirstNameEN = p.FirstNameEN,
                Photo = p.Photo,
                FileNo = p.FileNo,
                EntityEmpID = p.EntityEmpID,
                JobsID = p.JobsID,
                JobsName = lang == "ar" ? p.Jobs.NameAR : p.Jobs.NameEN,
               JobTypeId = p.Jobs.JobTypeId,
               FullName = lang == "ar" ? p.FirstNameAR + " " + p.LastNameAR : p.FirstNameEN + " " + p.LastNameEN,
               LastNameAR = p.LastNameAR,
               LastNameEN = p.LastNameEN,

           }).FirstOrDefault();
            if(data != null)
            {
                return data;
            }
            else
            {
                data = Uow.Vendors.GetAll(x => !x.IsDeleted && x.UserId == UserID).Select(p => new ProfileViewModel()
                {
                    MobileNo = p.MobileNo,
                    Email = p.Email,
                    FirstNameAR = p.FirstNameAr,
                    FirstNameEN = p.FirstNameEn,
                    Photo = p.ProfilePic,
                    FileNo = p.MobileNo,
                    EntityEmpID = 0,
                    JobsID = 0,
                    //DepartmentsID = p.DepartmentsID,
                    JobsName = "",
                    JobTypeId = 0,
                    FullName = lang == "ar" ? p.FirstNameAr + " " + p.SeconedNameEn : p.FirstNameEn + " " + p.SeconedNameEn,
                    LastNameAR = p.SeconedNameAr,
                    LastNameEN = p.SeconedNameEn,
                    VendorID = p.VendorsID , 
                    VendorCityId = p.CityID,
                    VendorBlockId = p.BlockID.HasValue ? p.BlockID.Value : 0 ,
                    VendorLastName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn , 
                    VendorFirstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn, 
                }).FirstOrDefault();
                return data;
            }

        }
        public ProfileViewModel GetProfileViewModelByUser(int UserID, string lang, string imageViewPath)
        {
            var data = Uow.Employees.GetAll(x => !x.IsDeleted && x.UserId == UserID).Select(p => new ProfileViewModel()
            {
                MobileNo = p.MobileNo,
                Email = p.Email,
                FirstNameAR = p.FirstNameAR,
                FirstNameEN = p.FirstNameEN,
                Photo = imageViewPath + p.Photo,
                FileNo = p.FileNo,
                EntityEmpID = p.EntityEmpID,
                JobsID = p.JobsID,
                JobsName = lang == "ar" ? p.Jobs.NameAR : p.Jobs.NameEN,
                JobTypeId = p.Jobs.JobTypeId,
                FullName = lang == "ar" ? p.FirstNameAR + " " + p.LastNameAR : p.FirstNameEN + " " + p.LastNameEN,
                LastNameAR = p.LastNameAR,
                LastNameEN = p.LastNameEN,

            }).FirstOrDefault();
            if (data != null)
            {
                return data;
            }
            else
            {
                data = Uow.Vendors.GetAll(x => !x.IsDeleted && x.UserId == UserID).Select(p => new ProfileViewModel()
                {
                    MobileNo = p.MobileNo,
                    Email = p.Email,
                    FirstNameAR = p.FirstNameAr,
                    FirstNameEN = p.FirstNameEn,
                    Photo =imageViewPath + p.ProfilePic,
                    FileNo = p.MobileNo,
                    EntityEmpID = 0,
                    JobsID = 0,
                    JobsName = "",
                    JobTypeId = 0,
                    FullName = lang == "ar" ? p.FirstNameAr + " " + p.SeconedNameAr : p.FirstNameEn + " " + p.SeconedNameEn,
                    LastNameAR = p.SeconedNameAr,
                    LastNameEN = p.SeconedNameEn,
                    VendorID = p.VendorsID,
                    VendorCityId = p.CityID,
                    VendorBlockId = p.BlockID.HasValue ? p.BlockID.Value : 0,
                    VendorLastName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn,
                    VendorFirstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn,
                }).FirstOrDefault();
                return data;
            }
        }
        public IQueryable<EmployeesViewModel> GetAllEmployeesViewModelBySearch(int UserTypeId, int EntityID, int? jobTypeId, string[] ListRegionID, string[] ListCityID, string[] ListEntityID, string Name, string FileNo, string Mobile, string IdNo, string[] ListStatusID, string[] ListDepartmentsID, string[] ListJobsID, string lang)
        {
            if (ListRegionID != null)
            {
                if (ListRegionID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListRegionID = null;
                }
            }
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            if (ListEntityID != null)
            {
                if (ListEntityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListEntityID = null;
                }
            }
            if (ListStatusID != null)
            {
                if (ListStatusID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListStatusID = null;
                }
            }
            if (ListDepartmentsID != null)
            {
                if (ListDepartmentsID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListDepartmentsID = null;
                }
            }
            if (ListJobsID != null)
            {
                if (ListJobsID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListJobsID = null;
                }
            }
            var getData = Uow.Employees.GetAll(x => !x.IsDeleted
                                                            && (jobTypeId != null ? x.Jobs.JobTypeId == jobTypeId : false)
                                                   && (ListRegionID != null ? ListRegionID.Any(y => x.City.RegionID.ToString() == y) : true)
                                                   && (ListCityID != null ? ListCityID.Any(y => x.CityID.ToString() == y) : true)
                                                   && (!string.IsNullOrEmpty(Name) ? (x.FirstNameAR == Name || x.FirstNameEN == Name) : true)
                                                   && (!string.IsNullOrEmpty(FileNo) ? x.FileNo == FileNo : true)
                                                   //&& (ListDepartmentsID != null ? ListDepartmentsID.Any(y => x.DepartmentsID.ToString() == y) : true)
                                                   && (ListJobsID != null ? ListJobsID.Any(y => x.JobsID.ToString() == y) : true)
                                                   && (ListStatusID != null ? ListStatusID.Any(y => (Convert.ToInt32(x.IsEnable)).ToString() == y) : true)
              && (!string.IsNullOrEmpty(Mobile) ? x.MobileNo == Mobile : true)
               && (!string.IsNullOrEmpty(IdNo) ? x.IDNo == IdNo : true)
                , "City.Region,Nationality,Jobs").OrderByDescending(p => p.CreateDate)
            .Select(p => new EmployeesViewModel()
            {
                CityID = p.CityID,
                RegionID = p.City.RegionID,
                Lat = p.Lat,
                Lng = p.Lng,
                IsEnable = p.IsEnable,
                PhoneNumber = p.PhoneNumber,
                MobileNo = p.MobileNo,
                Email = p.Email,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                Notes = p.Notes,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID,
                AddressAR = p.AddressAR,
                AddressEN = p.AddressEN,
                EntityEmpGuid = p.EntityEmpGuid,
                FirstNameAR = p.FirstNameAR,
                FirstNameEN = p.FirstNameEN,
                Photo = p.Photo,
                Zoom = p.Zoom,
                CityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                RegionName = lang == "ar" ? p.City.Region.NameAR : p.City.Region.NameEN,
                NationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                LastNameAR = p.LastNameAR,
                MidNameAR = p.MidNameAR,
                BirthDateMilady = p.BirthDateMilady,
                BloodTypeId = p.BloodTypeId,
                BirthDateHijri = p.BirthDateHijri,
                 FileNo = p.FileNo,
                Gender = p.Gender,
                IDNo = p.IDNo,
                LastNameEN = p.LastNameEN,
                MidNameEN = p.MidNameEN,
                EntityEmpID = p.EntityEmpID,
               
                JobsID = p.JobsID,
                //DepartmentsID = p.DepartmentsID,
                JobsName = lang == "ar" ? p.Jobs.NameAR : p.Jobs.NameEN,
                //DepartmentsName = p.Departments != null ? (lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN) : "",
                JobTypeId = p.Jobs.JobTypeId,
                EntityEmpName = lang == "ar" ? p.FirstNameAR + " " + p.LastNameAR : p.FirstNameEN + " " + p.LastNameEN,
                IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
            });
            return getData;
        }
        public EmployeesViewModel GetEmployeesViewModelByGuid(Guid EntityEmpGuid, string lang, string MainPathView)
        {

            var getData = Uow.Employees.GetAll(x => !x.IsDeleted
                                                   && x.EntityEmpGuid == EntityEmpGuid
                , "City.Region,Nationality,Jobs").OrderByDescending(p => p.CreateDate)
            .Select(p => new EmployeesViewModel()
            {
                CityID = p.CityID,
                RegionID = p.City.RegionID,
                Lat = p.Lat,
                Lng = p.Lng,
                IsEnable = p.IsEnable,
                PhoneNumber = p.PhoneNumber,
                MobileNo = p.MobileNo,
                Email = p.Email,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                Notes = p.Notes,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID,
                AddressAR = p.AddressAR,
                AddressEN = p.AddressEN,
                EntityEmpGuid = p.EntityEmpGuid,
                FirstNameAR = p.FirstNameAR,
                FirstNameEN = p.FirstNameEN,
                Zoom = p.Zoom,
                CityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                RegionName = lang == "ar" ? p.City.Region.NameAR : p.City.Region.NameEN,
                NationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                LastNameAR = p.LastNameAR,
                MidNameAR = p.MidNameAR,
                BirthDateMilady = p.BirthDateMilady,
                BloodTypeId = p.BloodTypeId,
                BirthDateHijri = p.BirthDateHijri,
                 FileNo = p.FileNo,
                Gender = p.Gender,
                IDNo = p.IDNo,
                LastNameEN = p.LastNameEN,
                MidNameEN = p.MidNameEN,
                EntityEmpID = p.EntityEmpID,
              
                Photo = !string.IsNullOrEmpty(p.Photo) ? (MainPathView + p.Photo) : p.Photo,
                JobsID = p.JobsID,
                //DepartmentsID = p.DepartmentsID,
                JobsName = lang == "ar" ? p.Jobs.NameAR : p.Jobs.NameEN,
                //DepartmentsName = p.Departments != null ? (lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN) : "",
                JobTypeId = p.Jobs.JobTypeId,
            }).FirstOrDefault();
            return getData;
        }
        #region Dashboard
        public SummaryHomeCount GetSummaryHomeCount(int? CustomersID)
        {
            var data = new SummaryHomeCount();
            #region Orders
            data.CreateCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Create && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)).Count();
            data.AcceptCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Accept && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)).Count();
            data.BeingProcessedCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Being_Processed && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)).Count();
            data.ShippingCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Shipping && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)).Count();
            data.DeliveredCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Delivered && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)).Count();
            data.BeingDeliveryCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Being_Delivery && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)).Count();
            data.CancelCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Cancel && x.CancelTypeID == (int)CancelTypeEnum.Default && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)).Count();
            data.PendingCancelCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Cancel && x.CancelTypeID == (int)CancelTypeEnum.Auto && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)).Count();
            data.AssignCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Assign && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)).Count();
            data.OnWayStoreCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.OnWay_Store && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)).Count();
            #endregion
            #region Captains
            data.CertifiedCaptainCount = Uow.Drivers.GetAll(query => !query.IsDeleted && query.IsActive).Count();
            data.NewRequestsCount = Uow.Drivers.GetAll(query => !query.IsDeleted && (query.RequestStatusId == (int)RequestStatusEnum.New)).Count();
            data.UnderScrutinyCount = Uow.Drivers.GetAll(query => !query.IsDeleted && (query.RequestStatusId == (int)RequestStatusEnum.Under_Scrutiny)).Count();
            data.RejectedRequestsCount = Uow.Drivers.GetAll(query => !query.IsDeleted && (query.RequestStatusId == (int)RequestStatusEnum.Rejected)).Count();
            data.WaitingActivationCount = Uow.Drivers.GetAll(query => !query.IsDeleted && (query.RequestStatusId == (int)RequestStatusEnum.Approved)).Count();
            data.IsOnlineCount = Uow.Drivers.GetAll(query => !query.IsDeleted && query.IsOnline).Count();
            #endregion
            #region Transfer
            data.OrderPendingTransferCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && (x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice
            || x.InvoiceDetails.Any(y => y.InvoiceMaster.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation
            || y.InvoiceMaster.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin
            || y.InvoiceMaster.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid))).Count();

            data.OrderPendingTransferSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && (x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice
            || x.InvoiceDetails.Any(y => y.InvoiceMaster.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation
            || y.InvoiceMaster.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin
            || y.InvoiceMaster.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid))).Sum(z => z.PerStore);

            data.OrderTransferredCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.InvoiceDetails.Any(y => y.InvoiceMaster.InvoiceTypeId == (int)InvoiceTypeEnum.Paid)).Count();
            data.OrderTransferredSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.InvoiceDetails.Any(y => y.InvoiceMaster.InvoiceTypeId == (int)InvoiceTypeEnum.Paid)).Sum(z => z.PerStore);
            #endregion
            #region Invoices
            data.OrderNotInvoiceCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice).Count();
            data.OrderNotInvoiceSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice).Sum(z => z.PerStore);

            data.WaitingReviewCount = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted && x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation).Count();
            data.WaitingReviewSum = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted && x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation).Sum(z => z.PerStore);

            data.WaitingTransferApprovalCount = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted && x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin).Count();
            data.WaitingTransferApprovalSum = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted && x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin).Sum(z => z.PerStore);

            data.WaitingTransferConfirmationCount = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted && x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid).Count();
            data.WaitingTransferConfirmationSum = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted && x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid).Sum(z => z.PerStore);

            data.TransferredCount = Uow.ListTransfer.GetAll(x => !x.IsDeleted && x.InvoiceMaster.InvoiceTypeId == (int)InvoiceTypeEnum.Paid).Count();
            data.TransferredSum = Uow.ListTransfer.GetAll(x => !x.IsDeleted && x.InvoiceMaster.InvoiceTypeId == (int)InvoiceTypeEnum.Paid).Sum(z => z.InvoiceMaster.PerStore);
            #endregion
            #region HomeMade
            data.HomeMadeEarningsSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainTypeId == (int)CaptainTypeEnum.Home_Made && (x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Invoiced || x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice || x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Cash_Transfered)).Sum(z => z.PerHome);
            data.NotInvoiceEarningsSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainTypeId == (int)CaptainTypeEnum.Home_Made && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Invoiced || x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice).Sum(z => z.PerHome);
            data.InvoiceEarningsSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainTypeId == (int)CaptainTypeEnum.Home_Made && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Cash_Transfered).Sum(z => z.PerHome);
            data.DeliveryTaxSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainTypeId == (int)CaptainTypeEnum.Home_Made && (x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Invoiced || x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice || x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Cash_Transfered)).Sum(z => z.DeliveryVatValue);
            data.HomeMadeDeliveryPriceSum = (Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainTypeId == (int)CaptainTypeEnum.Home_Made && (x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Invoiced || x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice || x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Cash_Transfered)).Sum(z => z.DeliveryPrice)) - data.DeliveryTaxSum;
            data.HomeMadeTotalSum = data.HomeMadeEarningsSum + data.HomeMadeDeliveryPriceSum;
            #endregion
            #region ShippingCompany
            data.ShippingCompanyOrdersCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company).Count();
            data.ShippingCompanyPendingOrdersCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company && (x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice)).Count();
            data.ShippingCompanyDeliveryPriceSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company && (x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice)).Sum(z => z.ShippingCompanyPrice);
            #endregion
            #region CaptianTransfer
            data.CaptainPendingTransferCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainPaidID == (int)PaidStatusEnum.Created
            && (x.OrderStatusID == (int)OrderStatusEnum.Delivered || x.OrderStatusID == (int)OrderStatusEnum.Cancel) && x.DriverCharge > 0).Count();

            data.CaptainPendingTransferSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainPaidID == (int)PaidStatusEnum.Created
            && (x.OrderStatusID == (int)OrderStatusEnum.Delivered || x.OrderStatusID == (int)OrderStatusEnum.Cancel) && x.DriverCharge > 0).Sum(z => z.DriverCharge);

            data.CaptainTransferredCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && (x.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered || x.CaptainPaidID == (int)PaidStatusEnum.STC_Transfered)).Count();
            data.CaptainTransferredSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && (x.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered || x.CaptainPaidID == (int)PaidStatusEnum.STC_Transfered)).Sum(z => z.DriverCharge);
            #endregion
            return data;
        }
        public void GetAllSummaryChart(string lang, out int[] ListOrdersCount, out int[] ListCaptainsCount, out double[] OrdersCount, out double[] VendorsCount)
        {
            ListOrdersCount = new int[9];
            ListCaptainsCount = new int[6];
            OrdersCount = new double[3];
            VendorsCount = new double[2];

            ListOrdersCount[0] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Create).Count();
            ListOrdersCount[1] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Accept).Count();
            ListOrdersCount[2] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Being_Processed).Count();
            ListOrdersCount[3] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Shipping).Count();
            ListOrdersCount[4] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Assign).Count();
            ListOrdersCount[5] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.OnWay_Store).Count();
            ListOrdersCount[6] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Being_Delivery).Count();
            ListOrdersCount[7] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Delivered).Count();
            ListOrdersCount[8] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Cancel).Count();

            ListCaptainsCount[0] = Uow.Drivers.GetAll(query => !query.IsDeleted && query.IsActive).Count();
            ListCaptainsCount[1] = Uow.Drivers.GetAll(query => !query.IsDeleted && (query.RequestStatusId == (int)RequestStatusEnum.New)).Count();
            ListCaptainsCount[2] = Uow.Drivers.GetAll(query => !query.IsDeleted && (query.RequestStatusId == (int)RequestStatusEnum.Under_Scrutiny)).Count();
            ListCaptainsCount[3] = Uow.Drivers.GetAll(query => !query.IsDeleted && (query.RequestStatusId == (int)RequestStatusEnum.Rejected)).Count();
            ListCaptainsCount[4] = Uow.Drivers.GetAll(query => !query.IsDeleted && (query.RequestStatusId == (int)RequestStatusEnum.Approved)).Count();
            ListCaptainsCount[5] = Uow.Drivers.GetAll(query => !query.IsDeleted && query.IsOnline).Count();
           
            var ActiveVendorsCount = Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable).Count();
            var NotActiveVendorsCount = Uow.Vendors.GetAll(x => !x.IsDeleted && !x.IsEnable).Count();
            if (ActiveVendorsCount + NotActiveVendorsCount > 0)
            {
                var d = (((double)ActiveVendorsCount / ((double)ActiveVendorsCount + (double)NotActiveVendorsCount)) * 100);
                VendorsCount[0] = d;
                VendorsCount[1] = ((double)NotActiveVendorsCount / ((double)ActiveVendorsCount + (double)NotActiveVendorsCount)) * 100;
            }
            var DeliveredOrderCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Delivered).Count();
            var CancelOrderCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Cancel).Count();
            var NewOrderCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID != (int)OrderStatusEnum.Delivered && x.OrderStatusID != (int)OrderStatusEnum.Cancel).Count();
            if (DeliveredOrderCount + CancelOrderCount + NewOrderCount > 0)
            {
                OrdersCount[0] = ((double)DeliveredOrderCount / ((double)DeliveredOrderCount + (double)CancelOrderCount + (double)NewOrderCount)) * 100;
                OrdersCount[1] = ((double)CancelOrderCount / ((double)DeliveredOrderCount + (double)CancelOrderCount + (double)NewOrderCount)) * 100;
                OrdersCount[2] = ((double)NewOrderCount / ((double)DeliveredOrderCount + (double)CancelOrderCount + (double)NewOrderCount)) * 100;
            }
        }
        #endregion
        public Employees GetByUserId(int UserId , string include = "") => Uow.Employees.GetAll(x => x.UserId == UserId , include).FirstOrDefault();
        #endregion
    }
}
