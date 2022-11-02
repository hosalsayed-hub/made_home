using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Setting;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class PaymentConfigurationController : Controller
    {
        #region Declarations

        private readonly BlPaymentConfiguration blPaymentConfiguration;
        private readonly IConfiguration _configuration;
        private readonly ResultMessage result;
        private readonly BLGeneral bLGeneral;

        public PaymentConfigurationController(BlPaymentConfiguration blPaymentConfiguration, IConfiguration configuration, ResultMessage result, BLGeneral bLGeneral)
        {
            this.bLGeneral = bLGeneral;
            this.blPaymentConfiguration = blPaymentConfiguration;
            this.result = result;
            _configuration = configuration;
        }
        #endregion
        #region actions
        [CustomeAuthoriz((int)ControllerEnum.PaymentConfiguration, (int)ActionEnum.View)]
        public IActionResult Index() => View(new PaymentConfigurationViewModel());



        [HttpPost]
        public JsonResult LoadTable()
        {
            string MainPath = _configuration["ConfigImageView"];
            var data = blPaymentConfiguration.GetPaymentConfigurations().Select(p => new
            {
                id = p.PaymentConfigurationID,
                IBANnumber = p.IBANnumber,
                AccountNumber = p.AccountNumber,
                BanksID = p.BanksID,
                Attachment = MainPath + p.Attachment,
                AccountImage = MainPath + p.AccountImage,
                p.IsEnable,
                IsEnableString = Utility.CurrentLanguageCode == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
            }).ToList().FirstOrDefault();
            return Json(new
            {
                aaData = data
            });
        }
        [CustomeAuthoriz((int)ControllerEnum.PaymentConfiguration, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(PaymentConfigurationViewModel model)
        {
            #region Vaildation
            if (string.IsNullOrWhiteSpace(model.IBANnumber))
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.IBANnumberRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(model.AccountNumber))
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.AccountNumberRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (model.BanksID == 0)
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.BankRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }




            #endregion
            #region SaveData
            int PaymentConfigurationID = 0;
            string OperationType = "add";

            bool IsSuccess = false;
            if (model.PaymentConfigurationID == 0)
            {
                #region AddCity
                model.CreateDate = bLGeneral.GetDateTimeNow();
                model.UserId = User.GetUserIdInt();
                try
                {
                    var AccountImage = "";
                    if (!string.IsNullOrEmpty(model.AccountImage))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        AccountImage = FileName;
                        string MainPath = _configuration["ConfigImageSave"];
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.AccountImage,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.AccountImage = AccountImage;
                    }
                    var Attachment = "";
                    if (!string.IsNullOrEmpty(model.Attachment))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Attachment = FileName;
                        string MainPath = _configuration["ConfigImageSave"];
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.Attachment,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.Attachment = Attachment;
                    }

                    IsSuccess = blPaymentConfiguration.Insert(model, out PaymentConfigurationID);
                }
                catch (System.Exception ex)
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = ex.Message.ToString();
                }
                #endregion
            }
            else
            {
                #region UpdateConfiguration
                model.UpdateDate = bLGeneral.GetDateTimeNow();
                model.UserUpdate = User.GetUserIdInt();
                try
                {
                    var AccountImage = "";
                    if (!string.IsNullOrEmpty(model.AccountImage))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        AccountImage = FileName;
                        string MainPath = _configuration["ConfigImageSave"];
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.AccountImage,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.AccountImage = AccountImage;
                    }
                    var Attachment = "";
                    if (!string.IsNullOrEmpty(model.Attachment))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Attachment = FileName;
                        string MainPath = _configuration["ConfigImageSave"];
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.Attachment,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.Attachment = Attachment;
                    }
                    IsSuccess = blPaymentConfiguration.Update(model);
                }
                catch (System.Exception ex)
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = ex.Message.ToString();
                }
                OperationType = "update";
                #endregion
            }
            if (IsSuccess)
            {
                result.ResultType = ResultMessageType.success.ToString();
                result.Message = Resources.Homemade.SuccessSaveMessage;
            }
            else
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            #endregion
            return Json(new { result, model.IBANnumber, model.AccountNumber, PaymentConfigurationID, OperationType });
        }

        #endregion
        #region helper

        #endregion
    }
}
