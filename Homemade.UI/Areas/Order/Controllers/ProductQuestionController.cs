using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Order;
using Homemade.BLL.Vendor;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Order.Controllers
{
    public class ProductQuestionController : Controller
    {
        #region Declarations
        private readonly BLUser _bLUser;
        private readonly BlVendor _blVendor;
        private readonly BlProdQuestion _blProdQuestion;
        private readonly IConfiguration _configuration;
        private readonly ResultMessage result;
        public ProductQuestionController(BLUser bLUser, BlVendor blVendor, BlProdQuestion blProdQuestion, IConfiguration configuration, ResultMessage result)
        {
            _bLUser = bLUser;
            _blVendor = blVendor;
            _blProdQuestion = blProdQuestion;
            _configuration = configuration;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.VendorProductQuestion, (int)ActionEnum.View)]
        public IActionResult IndexVendor()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.ProductQuestion, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region Helpers
        public IActionResult GetQuestionsReplyPartial(string listVendorID)
        {
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            var model = _blProdQuestion.GetAllProdQuestionViewModelRepley(vendor, _configuration["ProductImageView"], _configuration["CustomerImageView"], _configuration["VendorImageView"], InfraStructure.Utility.CurrentLanguageCode);
            return PartialView("_QuestionsReplyPartial", model);
        }
        public IActionResult GetQuestionsReplyPartialVendor()
        {
            var user = _bLUser.GetUserInfo(User.GetUserIdInt());
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

            var model = _blProdQuestion.GetAllProdQuestionViewModelRepley(VendorsID, _configuration["ProductImageView"], _configuration["CustomerImageView"], _configuration["VendorImageView"], InfraStructure.Utility.CurrentLanguageCode);
            return PartialView("_QuestionsReplyPartialVendor", model);
        }
        public IActionResult GetQuestionsNotReplyPartial(string listVendorID)
        {
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            var model = _blProdQuestion.GetAllProdQuestionViewModelNotRepley(vendor, _configuration["ProductImageView"], _configuration["CustomerImageView"], _configuration["VendorImageView"], InfraStructure.Utility.CurrentLanguageCode);
            return PartialView("_QuestionsNotReplyPartial", model);
        }
        public IActionResult GetQuestionsNotReplyPartialVendor()
        {
            var user = _bLUser.GetUserInfo(User.GetUserIdInt());
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

            var model = _blProdQuestion.GetAllProdQuestionViewModelNotRepley(VendorsID, _configuration["ProductImageView"], _configuration["CustomerImageView"], _configuration["VendorImageView"], InfraStructure.Utility.CurrentLanguageCode);
            return PartialView("_QuestionsNotReplyPartialWithAdd", model);
        }
        public JsonResult GetQuestionsCount(bool IsVendor, bool IsReply, string listVendorID)
        {
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            if (IsVendor)
            {
                var user = _bLUser.GetUserInfo(User.GetUserIdInt());
                int UserTypeId = user.UserType;
                int VendorsID = 0;
                if (UserTypeId == (int)UserTypeEnum.Vendor)
                {
                    var vednor = _blVendor.GetVendorByUserId(User.GetUserIdInt());
                    if (vednor != null)
                    {
                        VendorsID = vednor.VendorsID;
                        vendor = new List<string> { VendorsID.ToString() }.ToArray();
                    }
                }
            }

            var obj = _blProdQuestion.GetQuestionsCount(IsReply, vendor);
            return Json(obj);
        }
        #endregion
        #region Actions
        [CustomeAuthoriz((int)ControllerEnum.VendorProductQuestion, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult UpdateAnswer(int? ProdQuestionID, string Answer, bool IsPublish)
        {
            try
            {
                if (ProdQuestionID != null)
                {
                    if (!string.IsNullOrEmpty(Answer))
                    {
                        var status = _blProdQuestion.UpdateQuestion((int)ProdQuestionID, InfraStructure.Utility.CurrentLanguageCode, _configuration["CustomerImageView"], _configuration["ProductImageView"], Answer, IsPublish, User.GetUserIdInt(), _configuration["FireBaseKey"]);
                        if (status)
                        {
                            result.Status = true;
                            result.ResultType = ResultMessageType.success.ToString();
                            result.Message = Resources.Homemade.SuccessSaveMessage;
                        }
                        else
                        {
                            result.Status = false;
                            result.ResultType = ResultMessageType.error.ToString();
                            result.Message = Resources.Homemade.FailSaveMessage;
                        }
                    }
                    else
                    {
                        result.Status = false;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Message = Resources.Homemade.Please_Insert_Answer;
                    }
                }
                else
                {
                    result.Status = false;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.InsertValidDataMessage;
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(result);
        }
        [CustomeAuthoriz((int)ControllerEnum.ProductQuestion, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult DeleteQuestions(int id)
        {
            var result = false;
            try
            {
                result = _blProdQuestion.Delete(id, User.GetUserIdInt());
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
        #endregion
    }
}
