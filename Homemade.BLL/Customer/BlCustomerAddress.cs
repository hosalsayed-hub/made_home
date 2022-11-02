using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Customer;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Customer;
using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Customer
{
   
    public class BlCustomerAddress
    {
        #region Declartion
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        public BlCustomerAddress(IUOW _uow, BLGeneral bLGeneral)
        {
            this.Uow = _uow;
            _bLGeneral = bLGeneral;
        }
        #endregion
        #region Get
        public List<UpdateCustomerLocation> GetAllCutomerAddress(int CusotmerID, string lang)
        {
            return Uow.CustomerLocation.GetAll(s => s.CustomersID == CusotmerID && s.IsEnable && !s.IsDeleted, "Block.City.Region,AddressTypes").Select(m => new UpdateCustomerLocation()
            {
                address = m.Address,
                name = m.Name,
                mobileNo = m.MobileNo,
                blockID = m.BlockID,
                cityID = m.Block.CityID,
                regionID = m.Block.City.RegionID,
                lat = m.Lat,
                lng = m.Lng,
                streetNo = m.StreetNo,
                uniqueSign = m.UniqueSign,
                addressTypesID = m.AddressTypesID,
                buildingNumber = m.BuildingNumber,
                addressID = m.CustomerLocationID,
                blockName = lang == "ar" ? m.Block.NameAR : m.Block.NameEN,
                cityName = lang == "ar" ? m.Block.City.NameAR : m.Block.City.NameEN,
                regionName = lang == "ar" ? m.Block.City.Region.NameAR : m.Block.City.Region.NameEN,
                addressTypesName = lang == "ar" ? m.AddressTypes.NameAR : m.AddressTypes.NameEN,
            }).ToList();
        }
        public UpdateCustomerLocation GetOneCutomerAddress(int AddressID, string lang)
        {
            return Uow.CustomerLocation.GetAll(s => s.CustomerLocationID == AddressID && !s.IsDeleted, "Block.City.Region").Select(m => new UpdateCustomerLocation()
            {
                address = m.Address,
                name = m.Name,
                mobileNo = m.MobileNo,
                blockID = m.BlockID,
                cityID = m.Block.CityID,
                regionID = m.Block.City.RegionID,
                lat = m.Lat,
                lng = m.Lng,
                streetNo = m.StreetNo,
                uniqueSign = m.UniqueSign,
                addressTypesID = m.AddressTypesID,
                buildingNumber = m.BuildingNumber,
                addressID = m.CustomerLocationID,
                blockName = lang == "ar" ? m.Block.NameAR : m.Block.NameEN,
                cityName = lang == "ar" ? m.Block.City.NameAR : m.Block.City.NameEN,
                regionName = lang == "ar" ? m.Block.City.Region.NameAR : m.Block.City.Region.NameEN,
            }).FirstOrDefault();
        }
        public List<CustomerAddressOrderApi> GetAllCutomerAddressOrder(int CusotmerID, string lang)
        {
            return Uow.CustomerLocation.GetAll(s => s.CustomersID == CusotmerID && s.IsEnable && !s.IsDeleted, "AddressTypes").Select(m => new CustomerAddressOrderApi()
            {
                address = m.Address,
                name = m.Name,
                mobileNo = m.MobileNo,
                lat = m.Lat,
                lng = m.Lng,
                streetNo = m.StreetNo,
                uniqueSign = m.UniqueSign,
                addressType = lang == "ar" ? m.AddressTypes.NameAR : m.AddressTypes.NameEN,
                buildingNumber = m.BuildingNumber,
                addressID = m.CustomerLocationID,
            }).ToList();
        }
        #endregion
        #region Action
        public bool AddCustomerAddress(CustomerAddressViewModelApi model, int CustomerID, int userID)
        {
            try
            {
                var Addmodel = new CustomerLocation()
                {
                    Address = model.address,
                    AddressTypesID = model.addressTypesID,
                    BlockID = model.blockID,
                    BuildingNumber = model.buildingNumber,
                    StreetNo = model.streetNo,
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    IsDeleted = false,
                    IsEnable = true,
                    IsVerfiy = false,
                    IsLocation = true,
                    CustomerLocationGuid = Guid.NewGuid(),
                    CustomersID = CustomerID,
                    Lat = model.lat,
                    Lng = model.lng,
                    MobileNo = model.mobileNo,
                    Name = model.name,
                    UniqueSign = model.uniqueSign,
                    UserId = userID
                };
                Uow.CustomerLocation.Insert(Addmodel);
                Uow.Commit();
                return true;
            }
            catch (Exception wx)
            {

                return false;
            }
        }
        public bool UpdateCustomerAddress(UpdateCustomerLocation model, int CustomerID, int userID)
        {
            try
            {
                var CustomerData = Uow.CustomerLocation.GetAll(s => s.CustomerLocationID == model.addressID && !s.IsDeleted).FirstOrDefault();
                if (CustomerData != null)
                {
                    CustomerData.Address = model.address;
                    CustomerData.AddressTypesID = model.addressTypesID;
                    CustomerData.BlockID = model.blockID;
                    CustomerData.BuildingNumber = model.buildingNumber;
                    CustomerData.StreetNo = model.streetNo;
                    CustomerData.UpdateDate = _bLGeneral.GetDateTimeNow();
                    CustomerData.IsDeleted = false;
                    CustomerData.IsEnable = true;
                    CustomerData.IsVerfiy = false;
                    CustomerData.CustomersID = CustomerID;
                    CustomerData.Lat = model.lat;
                    CustomerData.Lng = model.lng;
                    CustomerData.MobileNo = model.mobileNo;
                    CustomerData.Name = model.name;
                    CustomerData.UniqueSign = model.uniqueSign;
                    CustomerData.UserUpdate = userID;
                    Uow.CustomerLocation.Update(CustomerData);
                    Uow.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception wx)
            {

                return false;
            }
        }
        public bool DeleteLocation(int AddressID, int userID)
        {
            try
            {
                var CustomerData = Uow.CustomerLocation.GetAll(s => s.CustomerLocationID == AddressID && !s.IsDeleted).FirstOrDefault();
                CustomerData.IsDeleted = true;
                CustomerData.DeleteDate = _bLGeneral.GetDateTimeNow();
                CustomerData.UserDelete = userID;
                Uow.CustomerLocation.Update(CustomerData);
                Uow.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool InsertInquiry(InquiryViewModelApi model, int userID)
        {
            try
            {
                var Inquiry = new Inqueries()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    Email = model.email,
                    InqueriesGuid = Guid.NewGuid(),
                    Message = model.message,
                    MobileNo = model.mobileNo,
                    Name = model.name
                };
                Uow.Inqueries.Insert(Inquiry);
                Uow.Commit();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion
    }
}
