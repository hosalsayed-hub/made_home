using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homemade.BLL;
 using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Customer;
using Homemade.UI.Attributes;
using Microsoft.Extensions.Configuration;
using Homemade.UI.InfraStructure;
using Homemade.BLL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Homemade.Model;
using Homemade.BLL.Emp;
using Homemade.BLL.Customer;
using Homemade.BLL.Vendor;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.ComponentModel;
using Homemade.BLL.Order;
using System.Data;

namespace Homemade.UI.Areas.Customer.Controllers
{
    public class CustomerController : Controller
    {
        #region Declarations
        private readonly BlCustomer blCustomers;
        private readonly ResultMessage result;
        private readonly BLPermission _bLPermission;
        private readonly IConfiguration _configuration;
        private readonly BLUser bLUser;
        private readonly BlVendor _blVendor;
        private readonly BlOrders _blOrders;
        private readonly UserManager<User> _userManager;
        private readonly BLGeneral bLGeneral;
        
        public CustomerController(BLPermission BLPermission, UserManager<User> userManager, BlCustomer blCustomers, ResultMessage result, BLGeneral bLGeneral, IConfiguration configuration, BLUser bLUser, BlVendor blVendor, BlOrders blOrders)
        {
            this.bLGeneral = bLGeneral;
            _configuration = configuration;
            _userManager = userManager;
            this.blCustomers = blCustomers;
            this.result = result;
            this._bLPermission = BLPermission;
            this.bLUser = bLUser;
            _blVendor = blVendor;
            _blOrders = blOrders;
        }
        #endregion
        #region View
        public IActionResult Index()
        {           
             return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.VendorCustomer, (int)ActionEnum.View)]
        public IActionResult IndexVendor()
        {
            return View();
        }
        public IActionResult Details(Guid? id)
        {
                if (id.HasValue)
                {
                    CustomerViewModel CustomerViewModel = blCustomers.GetCustomersViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode , _configuration["CustomerImageView"] , _configuration["ProductImageView"]);
                    if (CustomerViewModel != null)
                    {
                        return View(CustomerViewModel);
                    }
                }
                return Redirect(Url.Action("Index", "Customer", new { Area = "Customers" }));
        }
        [CustomeAuthoriz((int)ControllerEnum.CustomerBalance, (int)ActionEnum.View)]
        public IActionResult CustomerBalance()
        {
            return View();
        }

        #endregion
        #region Permession
        public JsonResult LoadTableRolePermissions(string Roles, int? userId)
        {
            List<PermissionControllerActionViewModel> controller = new List<PermissionControllerActionViewModel>();
            if (!string.IsNullOrWhiteSpace(Roles))
            {
                controller = _bLPermission.GetAllPermissionForComapny(Roles, userId, Request.Cookies.IsArabic());
            }
            var data = controller;
            int totalCount = data.Count();

            if (Request.Form.ContainsKey("sSearch"))
            {
                string s = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(s))
                {
                    data = data.Where(x => x.ControllerName.ToLower().Contains(s.ToLower())).ToList();
                }
            }

            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = data
            });
        }

        #endregion
        #region Helpers
        [HttpPost]
        public JsonResult LoadTableOrdersByCustomer(int CustomersID)
        {
            #region DataTableParameters
            int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            int displayStart = int.Parse(Request.Form["iDisplayStart"]);
            var isfirst = false;
            if (displayStart == 0)
            {
                displayStart = displayLength;
                isfirst = true;
            }
            int pageno = displayStart / displayLength;
            if (!isfirst)
            {
                pageno = pageno + 1;
            }
            #endregion
            var data = _blOrders.GetAllOrdersVendorViewModelByCustomer(CustomersID, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x => x.VendorsName.ToLower().Contains(search.ToLower()) ||
                    x.Price.ToString().ToLower().Contains(search.ToLower()) || x.Discount.ToString().ToLower().Contains(search.ToLower()) || x.Vat.ToString().ToLower().Contains(search.ToLower())
                    || x.DeliveryPrice.ToString().ToLower().Contains(search.ToLower()));
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var takenData = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });

        }
        [HttpPost]
        public JsonResult LoadTable()
        {
            #region DataTableParameters

            
            int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            int displayStart = int.Parse(Request.Form["iDisplayStart"]);
           
            var isfirst = false;
            if (displayStart == 0)
            {
                displayStart = displayLength;
                isfirst = true;
            }
            int pageno = displayStart / displayLength;
            if (!isfirst)
            {
                pageno = pageno + 1;
            }
            #endregion
            string[] city = null;
             

            var data = blCustomers.GetAllCustomersViewModelBySearch( city , "" , "", InfraStructure.Utility.CurrentLanguageCode).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.FirstName.ToLower().Contains(search.ToLower()) || x.CityName.ToLower().Contains(search.ToLower()) 
                    || x.MobileNo.ToLower().Contains(search.ToLower()) || x.Address.ToLower().Contains(search.ToLower())
                    || x.Email.ToLower().Contains(search.ToLower()) || x.IsEnableString.ToLower().Contains(search.ToLower())).ToList();
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var takenData = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });

        }
        [HttpPost]
        public JsonResult LoadTableVendor(string listCityID, string name, string mobile)
        {
            #region DataTableParameters
            int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            int displayStart = int.Parse(Request.Form["iDisplayStart"]);
            var isfirst = false;
            if (displayStart == 0)
            {
                displayStart = displayLength;
                isfirst = true;
            }
            int pageno = displayStart / displayLength;
            if (!isfirst)
            {
                pageno = pageno + 1;
            }
            #endregion
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null")
            {
                city = listCityID.Split(',');
            }
            var user = bLUser.GetUserInfo(User.GetUserIdInt());
            int UserTypeId = user.UserType;
            int VendorsID = 0;
            if (UserTypeId == (int)UserTypeEnum.Vendor)
            {
                var vednor = _blVendor.GetVendorByUserId(User.GetUserIdInt());
                if (vednor != null)
                {
                    VendorsID = vednor.VendorsID;
                }
            }

            var data = blCustomers.GetAllCustomersViewModelByVendorAndSearch(VendorsID, city, name, mobile, InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.FirstName.ToLower().Contains(search.ToLower()) || x.CityName.ToLower().Contains(search.ToLower())
                    || x.MobileNo.ToLower().Contains(search.ToLower()) || x.Address.ToLower().Contains(search.ToLower())
                    || x.Email.ToLower().Contains(search.ToLower()) || x.IsEnableString.ToLower().Contains(search.ToLower()));
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var takenData = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });

        }
        [HttpPost]
        public JsonResult LoadTableCustomerBalance()
        {
            #region DataTableParameters


            int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            int displayStart = int.Parse(Request.Form["iDisplayStart"]);

            var isfirst = false;
            if (displayStart == 0)
            {
                displayStart = displayLength;
                isfirst = true;
            }
            int pageno = displayStart / displayLength;
            if (!isfirst)
            {
                pageno = pageno + 1;
            }
            #endregion
            var data = blCustomers.GetAllCustomersViewModel(InfraStructure.Utility.CurrentLanguageCode).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.FirstName.ToLower().Contains(search.ToLower()) || x.CityName.ToLower().Contains(search.ToLower())
                    || x.MobileNo.ToLower().Contains(search.ToLower()) || x.Address.ToLower().Contains(search.ToLower())
                    || x.Email.ToLower().Contains(search.ToLower()) || x.IsEnableString.ToLower().Contains(search.ToLower())).ToList();
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var takenData = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });

        }
        private JsonResult ValidateModel(int EntityCustomerID , string firstName , string secondName , int CityID, string Mobile, string Email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                result.Message = BLL.Resources.HomemadeErrorMessages.FirstNameRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(secondName))
            {
                result.Message = BLL.Resources.HomemadeErrorMessages.SecondNameRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (!string.IsNullOrWhiteSpace(Mobile))
            {
                if (blCustomers.IsExistMobile(Mobile, EntityCustomerID))
                {
                    result.Message = Homemade.UI.Resources.Homemade.MobileIsExists;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                result.Message = Homemade.UI.Resources.Homemade.EmailRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (blCustomers.IsExistEmail(Email, EntityCustomerID))
            {
                result.Message = Homemade.UI.Resources.Homemade.EmailExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }

            if (CityID == 0)
            {
                result.Message = BLL.Resources.HomemadeErrorMessages.CityRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            return null;
        }
        public bool GetPermissionUser(int action)
        {
            var user = bLUser.GetUserInfo(User.GetUserIdInt());
            int UserTypeId = user.UserType;
            int controller = (int)ControllerEnum.Customer;
             
             
            return _bLPermission.GetPermissionByUserAndController(user.UserName, controller, action);
        }
        #endregion
        #region Action
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Delete)]
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var result = false;
            try
            {
                Guid vendorNewGuid = Guid.NewGuid();
                result = blCustomers.Delete(id, User.GetUserIdInt(), vendorNewGuid);
                if (result)
                {
                    var vendor = _blVendor.GetById(id);
                    if (vendor != null)
                    {
                        User user = bLUser.GetUserInfo(vendor.UserId);
                        if (user != null)
                        {
                            user.UserName = vendorNewGuid.ToString();
                            user.NormalizedUserName = vendorNewGuid.ToString();
                            await _userManager.UpdateAsync(user);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blCustomers.ChangeStatus(id, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessChangeMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailChangeStatueMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);

        }
        [HttpPost]
        public JsonResult AddCustomerBalance(int id, decimal shippingAmount, string description)
        {
            var result = false;
            try
            {
                if (shippingAmount > 0)
                {
                    if (shippingAmount <= 2000)
                    {
                        result = blCustomers.AddCustomerBalance(id, shippingAmount, description, User.GetUserIdInt());
                    }
                    else
                    {
                        return Json(new ResultMessage { Message = Resources.Homemade.Shipping_Amount_Not_Valid, ResultType = ResultMessageType.error.ToString() });
                    }
                }
                else
                {
                    return Json(new ResultMessage { Message = Resources.Homemade.Please_Insert_Shipping_Amount, ResultType = ResultMessageType.error.ToString() });
                }
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.Save_Changes, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailSaveMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);

        }
        #endregion
        #region Excel
        #region Basic Excel
        public static byte[] ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, params string[] columnsToTake)
        {
            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {

                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;

                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("SR No", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }

                // add the content into the Excel file  
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                // autofit width of cells with small content  
                int columnIndex = 1;
                foreach (DataColumn column in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];

                    if (!string.IsNullOrWhiteSpace(columnCells.Max(cell => cell.Value.ToString())))
                    {
                        int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());
                        if (maxLength < 150)
                        {
                            workSheet.Column(columnIndex).AutoFit();

                        }
                    }

                    columnIndex++;
                }
                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }
                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                }

                result = package.GetAsByteArray();
            }
            return result;
        }
        public static byte[] ExportExcel<T>(List<T> data, string Heading = "", bool showSlno = false, params string[] ColumnsToTake)
        {
            return ExportExcel(ListToDataTable<T>(data), Heading, showSlno, ColumnsToTake);
        }
        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }
        #endregion
        public FileContentResult ExcelCustomers()
        {

            byte[] filecontent = null;
            string downloadFileName = "";
            try
            {
                string[] city = null;

                var data = blCustomers.GetAllCustomersViewModelBySearch(city, "", "", InfraStructure.Utility.CurrentLanguageCode).ToList();
                var user = bLUser.GetUserInfo(User.GetUserIdInt());
                int UserTypeId = user.UserType;
                int VendorsID = 0;
                if (UserTypeId == (int)UserTypeEnum.Vendor)
                {
                    var vednor = _blVendor.GetVendorByUserId(User.GetUserIdInt());
                    if (vednor != null)
                    {
                        VendorsID = vednor.VendorsID;
                        data = blCustomers.GetAllCustomersViewModelByVendorAndSearch(VendorsID, city, "", "", InfraStructure.Utility.CurrentLanguageCode).ToList();
                    }
                }

                if (data != null)
                {
                    if (UserTypeId == (int)UserTypeEnum.Vendor)
                    {
                        int totalCount = data.Count();
                        var listexcel = data.Select(x => new
                        {
                            name = x.FirstName,
                            city = x.CityName,
                            address = x.Address,
                            status = x.IsEnableString,
                        }).ToList();
                        string[] column = { "Name", "City", "Address", "Status" };
                        filecontent = ExportExcel(listexcel, "Selected count : " + totalCount, true, column);
                        downloadFileName = "CustomersList_" + bLGeneral.GetDateTimeNow() + ".xlsx";
                        return File(filecontent, ExcelContentType, downloadFileName);
                    }
                    else
                    {
                        int totalCount = data.Count();
                        var listexcel = data.Select(x => new
                        {
                            name = x.FirstName,
                            email = x.Email,
                            mobileNo = x.MobileNo,
                            createDate = x.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                            deliveredCount = x.DeliveredCount,
                            status = x.IsEnableString,
                        }).ToList();
                        string[] column = { "Name", "Email", "Mobile No", "Create Date", "Delivered", "Status" };
                        filecontent = ExportExcel(listexcel, "Selected count : " + totalCount, true, column);
                        downloadFileName = "CustomersList_" + bLGeneral.GetDateTimeNow() + ".xlsx";
                        return File(filecontent, ExcelContentType, downloadFileName);
                    }
                }
                else
                {
                    string[] columns = { "Name", "Email", "Mobile No", "Create Date", "Delivered", "Status" };
                    filecontent = ExportExcel(data.ToList(), "Customers List ", true, columns);
                    downloadFileName = "CustomersList_" + bLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
            }
            catch (Exception e)
            {
                string[] columns = { "Name", "Email", "Mobile No", "Create Date", "Delivered", "Status" };
                filecontent = ExportExcel(null, "Customers List ", true, columns);
                downloadFileName = "CustomersList_" + bLGeneral.GetDateTimeNow() + ".xlsx";
                return File(filecontent, ExcelContentType, downloadFileName);
            }
        }
        #endregion
    }
}
