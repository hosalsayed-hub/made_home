using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.Order;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Order;
using Homemade.Model.Setting;

namespace Homemade.BLL.Order
{
    public class BlProdQuestion
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        private readonly BlTokens _blTokens;
        public BlProdQuestion(IUOW _uow, BLGeneral blGeneral, BlTokens blTokens)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
            _blTokens = blTokens;
        }
        #endregion
        #region Helpers
        public ProdQuestion GetById(int id) => Uow.ProdQuestion.GetAll(x => x.ProdQuestionID == id).FirstOrDefault();
        public IQueryable<ProdQuestionViewModel> GetAllProdQuestionViewModelRepley(int VendorsID, string ProdPath, string customerPath, string vendorPath, string lang)
        {
            var data = Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.Product.VendorsID == VendorsID && s.IsRepley,
                       "Product,Customers").Select(m => new ProdQuestionViewModel()
                       {
                           CustomerName = m.Customers.FirstName + " " + m.Customers.SeconedName,
                           QuestionDate = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                           ProductImage = !string.IsNullOrWhiteSpace(m.Product.Logo) ? ProdPath + m.Product.Logo : "",
                           Question = m.Question,
                           ProdQuestionID = m.ProdQuestionID,
                           IsRepley = m.IsRepley,
                           Answer = m.Answer,
                           ProductName = lang == "ar" ? m.Product.NameAr : m.Product.NameEn,
                           CustomersID = m.CustomersID,
                           ProdQuestionGuid = m.ProdQuestionGuid,
                           ProductID = m.ProductID,
                           CreateDate = m.CreateDate,
                           VendorName = lang == "ar" ? m.Product.Vendors.StoreNameAr : m.Product.Vendors.StoreNameEn,
                           CustomerImage = !string.IsNullOrWhiteSpace(m.Customers.ProfilePic) ? (customerPath + m.Customers.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           VendorImage = !string.IsNullOrWhiteSpace(m.Product.Vendors.ProfilePic) ? (vendorPath + m.Product.Vendors.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           ProductGuid = m.Product.ProductGuid,
                           VendorsGuid = m.Product.Vendors.VendorsGuid,
                       }).OrderByDescending(x => x.CreateDate);
            return data;
        }
        public IQueryable<ProdQuestionViewModel> GetAllProdQuestionViewModelRepley(string[] ListVendorID, string ProdPath, string customerPath, string vendorPath, string lang)
        {
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            var data = Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.IsRepley
             && (ListVendorID != null ? ListVendorID.Any(y => s.Product.VendorsID.ToString() == y) : true),
                       "Product,Customers").Select(m => new ProdQuestionViewModel()
                       {
                           CustomerName = m.Customers.FirstName + " " + m.Customers.SeconedName,
                           QuestionDate = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                           ProductImage = !string.IsNullOrWhiteSpace(m.Product.Logo) ? ProdPath + m.Product.Logo : "",
                           Question = m.Question,
                           ProdQuestionID = m.ProdQuestionID,
                           IsRepley = m.IsRepley,
                           Answer = m.Answer,
                           ProductName = lang == "ar" ? m.Product.NameAr : m.Product.NameEn,
                           CustomersID = m.CustomersID,
                           ProdQuestionGuid = m.ProdQuestionGuid,
                           ProductID = m.ProductID,
                           VendorName = lang == "ar" ? m.Product.Vendors.StoreNameAr : m.Product.Vendors.StoreNameEn,
                           CustomerImage = !string.IsNullOrWhiteSpace(m.Customers.ProfilePic) ? (customerPath + m.Customers.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           VendorImage = !string.IsNullOrWhiteSpace(m.Product.Vendors.ProfilePic) ? (vendorPath + m.Product.Vendors.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           CreateDate = m.CreateDate,
                           ProductGuid = m.Product.ProductGuid,
                           VendorsGuid = m.Product.Vendors.VendorsGuid,
                       }).OrderByDescending(x => x.CreateDate);
            return data;
        }
        public IQueryable<ProdQuestionViewModel> GetAllProdQuestionViewModelNotRepley(int VendorsID, string ProdPath, string customerPath, string vendorPath, string lang)
        {
            return Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.Product.VendorsID == VendorsID && !s.IsRepley,
                       "Product.Vendors,Customers").Select(m => new ProdQuestionViewModel()
                       {
                           CustomerName = m.Customers.FirstName + " " + m.Customers.SeconedName,
                           QuestionDate = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                           ProductImage = !string.IsNullOrWhiteSpace(m.Product.Logo) ? ProdPath + m.Product.Logo : "/assets/homeMadeSite/img/logo.svg",
                           Question = m.Question,
                           ProdQuestionID = m.ProdQuestionID,
                           IsRepley = m.IsRepley,
                           Answer = m.Answer,
                           ProductName = lang == "ar" ? m.Product.NameAr : m.Product.NameEn,
                           CustomersID = m.CustomersID,
                           ProdQuestionGuid = m.ProdQuestionGuid,
                           ProductID = m.ProductID,
                           VendorName = lang == "ar" ? m.Product.Vendors.StoreNameAr : m.Product.Vendors.StoreNameEn,
                           CustomerImage = !string.IsNullOrWhiteSpace(m.Customers.ProfilePic) ? (customerPath + m.Customers.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           VendorImage = !string.IsNullOrWhiteSpace(m.Product.Vendors.ProfilePic) ? (vendorPath + m.Product.Vendors.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                      CreateDate = m.CreateDate,
                           ProductGuid = m.Product.ProductGuid,
                           VendorsGuid = m.Product.Vendors.VendorsGuid,
                       }).OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<ProdQuestionViewModel> GetAllProdQuestionViewModelNotRepley(string[] ListVendorID, string ProdPath, string customerPath, string vendorPath, string lang)
        {
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            return Uow.ProdQuestion.GetAll(s => !s.IsDeleted && !s.IsRepley
            && (ListVendorID != null ? ListVendorID.Any(y => s.Product.VendorsID.ToString() == y) : true),
                       "Product,Customers").Select(m => new ProdQuestionViewModel()
                       {
                           CustomerName = m.Customers.FirstName + " " + m.Customers.SeconedName,
                           QuestionDate = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                           ProductImage = !string.IsNullOrWhiteSpace(m.Product.Logo) ? ProdPath + m.Product.Logo : "",
                           Question = m.Question,
                           ProdQuestionID = m.ProdQuestionID,
                           IsRepley = m.IsRepley,
                           Answer = m.Answer,
                           ProductName = lang == "ar" ? m.Product.NameAr : m.Product.NameEn,
                           CustomersID = m.CustomersID,
                           ProdQuestionGuid = m.ProdQuestionGuid,
                           ProductID = m.ProductID,
                           VendorName = lang == "ar" ? m.Product.Vendors.StoreNameAr : m.Product.Vendors.StoreNameEn,
                           CustomerImage = !string.IsNullOrWhiteSpace(m.Customers.ProfilePic) ? (customerPath + m.Customers.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           VendorImage = !string.IsNullOrWhiteSpace(m.Product.Vendors.ProfilePic) ? (vendorPath + m.Product.Vendors.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           CreateDate = m.CreateDate,
                           ProductGuid = m.Product.ProductGuid,
                           VendorsGuid = m.Product.Vendors.VendorsGuid,
                       }).OrderByDescending(x => x.CreateDate);
        }
        public int GetQuestionsCount(bool IsReply, string[] ListVendorID)
        {
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            return Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.IsRepley == IsReply
             && (ListVendorID != null ? ListVendorID.Any(y => s.Product.VendorsID.ToString() == y) : true)).Count();
        }
        #endregion
        #region Actions
        public bool UpdateQuestion(int qeuestionId, string lang, string custoemrPath, string ProdPath, string Relpey, bool IsPublish, int userid, string FireBaseKey)
        {
            var Data = Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.ProdQuestionID == qeuestionId, "Product,Customers").FirstOrDefault();
            Data.IsRepley = true;
            Data.Answer = Relpey;
            Data.IsEnable = IsPublish;
            Data.UserUpdate = userid;
            Data.UpdateDate = _blGeneral.GetDateTimeNow();
            Uow.ProdQuestion.Update(Data);
            var Noti = new Notification()
            {
                CreateDate = _blGeneral.GetDateTimeNow(),
                CustomersID = Data.CustomersID,
                TitleAR = "تم الرد علي سؤالك بخصوص المنتج " + Data.Product.NameAr + "",
                TitleEN = "Vendor has repley to your question on product " + Data.Product.NameEn + "",
                DescEN = Relpey,
                DescAR = Relpey,
                IsDeleted = false,
                IsEnable = true,
                IsSeen = false,
                NotificationGuid = Guid.NewGuid(),
                NotificationTypeID = (int)NotificationTypeEnum.Question,
                UserId = userid,
                NotificationDate = _blGeneral.GetDateTimeNow(),
                ProdQuestionID = Data.ProdQuestionID
            };
            Uow.Notification.Insert(Noti);
            var _PushListVendor = new PushList()
            {
                orderId = Data.ProdQuestionID,
                lat = 0,
                lng = 0,
                trackNo = Data.ProdQuestionID.ToString(),
                profit = "",
                cusotmerAddress = "",
                vendorAddress = "",
                vendorLogo = "",
                vendorName = "",
                body = "تم الرد علي سؤالك بخصوص المنتج " + Data.Product.NameAr + "",
                sound = "default", //For IOS
                title = " الرد علي سؤالك",
                content_available = "true", //For Android
                priority = "high", //For Android,
                serverKey = FireBaseKey,
                estimation = 20,
                pushType = (int)PushTypeEnum.Question
            };
            var UserDataVendor = _blTokens.GetMobileDataByUserID(Data.Customers.UserId);
            _blGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
            Uow.Commit();
            return true;
        }
        public bool Delete(int id, int userId)
        {
            var prodQuestion = GetById(id);
            if (prodQuestion != null)
            {
                prodQuestion.IsDeleted = true;
                prodQuestion.UserDelete = userId;
                prodQuestion.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.ProdQuestion.Update(prodQuestion);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
