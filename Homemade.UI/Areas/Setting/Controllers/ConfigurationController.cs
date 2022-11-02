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
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class configurationController : Controller
    {
        #region Declarations
        private readonly BlConfiguration blConfiguration;
        private readonly IConfiguration _configuration;
        private readonly ResultMessage result;
        private readonly BLGeneral bLGeneral;
        private readonly BlDiscount _blDiscount;


        public configurationController(BlConfiguration blConfiguration, BlDiscount blDiscount, IConfiguration configuration, ResultMessage result, BLGeneral bLGeneral)
        {
            this.bLGeneral = bLGeneral;
            this.blConfiguration = blConfiguration;
            this.result = result;
            _configuration = configuration;
            _blDiscount = blDiscount;

        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Configuration, (int)ActionEnum.View)]
        public IActionResult Index() => View(new ConfigurationViewModel());
        #endregion
        #region Actions
        [HttpPost]
        public JsonResult LoadTable()
        {
            #region DataTableParameters
            //int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            //int displayStart = int.Parse(Request.Form["iDisplayStart"]);
            //var isfirst = false;
            //if (displayStart == 0)
            //{
            //    displayStart = displayLength;
            //    isfirst = true;
            //}
            //int pageno = displayStart / displayLength;
            //if (!isfirst)
            //{
            //    pageno = pageno + 1;
            //}
            #endregion
            var data = blConfiguration.GetConfigurations().Select(p => new
            {
                id = p.ConfigurationID,
                nameAr = p.CompanNameAr,
                nameEn = p.CompanNameEn,
                PhoneNumber = p.PhoneNumber,
                Fax = p.Fax,
                MobileNumber = p.MobileNumber,
                CRNumber = p.CRNumber,
                Address = p.Address,
                StreetNo = p.StreetNo,
                CRImage = !string.IsNullOrWhiteSpace(p.CRImage) ? _configuration["ConfigImageView"] + p.CRImage : "",
                Banner = !string.IsNullOrWhiteSpace(p.Banner) ? _configuration["ConfigImageView"] + p.Banner : "",
                Logo = !string.IsNullOrWhiteSpace(p.Logo) ? _configuration["ConfigImageView"] + p.Logo : "",
                SeconedEmail = p.SeconedEmail,
                Email = p.Email,
                p.IsEnable,
                DeliveryPrice = p.DeliveryPrice,
                MinDeliveryPrice = p.MinDeliveryPrice,
                MaxDeliveryPrice = p.MaxDeliveryPrice,
                name = Utility.CurrentLanguageCode == "ar" ? p.CompanNameAr : p.CompanNameEn,
                IsEnableString = Utility.CurrentLanguageCode == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
                DeliveryPriceVatPercent = p.DeliveryPriceVatPercent,
                DeliveryPriceWithoutVat = p.DeliveryPriceWithoutVat,
                MinKM = p.MinKM,
                OverKmFare = p.OverKmFare,
                WhatsappLink = p.WhatsappLink,
                TwitterLink = p.TwitterLink,
                InstagramLink = p.InstagramLink,
                SnapchatLink = p.SnapchatLink,
            }).ToList().FirstOrDefault();

            return Json(new
            {
                aaData = data
            });
        }

        [CustomeAuthoriz((int)ControllerEnum.Configuration, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(ConfigurationViewModel model)
        {
            #region Vaildation
            if (string.IsNullOrWhiteSpace(model.CompanNameAr))
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(model.CompanNameEn))
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(model.Address))
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.AddressRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }

            //if (decimal.Parse(model.DeliveryPrice, CultureInfo.InvariantCulture) < 1)
            //{
            //    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.DeliveryPriceBiggerThanZero;
            //    result.ResultType = ResultMessageType.error.ToString();
            //    return Json(result);
            //}

            if (decimal.Parse(model.MinDeliveryPrice, CultureInfo.InvariantCulture) < 1)
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.MinDeliveryPriceBiggerThanZero;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }

            if (decimal.Parse(model.MaxDeliveryPrice, CultureInfo.InvariantCulture) < 1)
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.MaxDeliveryPriceBiggerThanZero;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }

            #endregion
            #region SaveData
            int ConfigurationID = 0;
            string OperationType = "add";

            bool IsSuccess = false;
            if (model.ConfigurationID == 0)
            {
                #region AddConfiguration
                model.CreateDate = bLGeneral.GetDateTimeNow();
                model.UserId = User.GetUserIdInt();
                try
                {
                    string MainPath = _configuration["ConfigImageSave"];

                    var Logo = "";
                    if (!string.IsNullOrEmpty(model.Logo))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Logo = FileName;
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.Logo,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.Logo = Logo;
                    }
                    var Banner = "";
                    if (!string.IsNullOrEmpty(model.Banner))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Banner = FileName;
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.Banner,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.Banner = Banner;
                    }
                    var CRImage = "";
                    if (!string.IsNullOrEmpty(model.CRImage))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        CRImage = FileName;
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.CRImage,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.CRImage = CRImage;
                    }
                    IsSuccess = blConfiguration.Insert(model, out ConfigurationID);
                }
                catch (System.Exception ex)
                {
                    //  _blErrorLog.Insert("UI", "Setting", "City", "Index", ex.Message, ex.InnerException?.Message);

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
                    string MainPath = _configuration["ConfigImageSave"];
                    var Logo = "";
                    if (!string.IsNullOrEmpty(model.Logo))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Logo = FileName;
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.Logo,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.Logo = Logo;
                    }
                    var Banner = "";
                    if (!string.IsNullOrEmpty(model.Banner))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Banner = FileName;
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.Banner,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.Banner = Banner;
                    }
                    var CRImage = "";
                    if (!string.IsNullOrEmpty(model.CRImage))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        CRImage = FileName;
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.CRImage,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.CRImage = CRImage;
                    }
                    IsSuccess = blConfiguration.Update(model);
                }
                catch (System.Exception ex)
                {
                    //   _blErrorLog.Insert("UI", "Setting", "City", "Index", ex.Message, ex.InnerException?.Message);

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
            return Json(result);
        }

        [CustomeAuthoriz((int)ControllerEnum.Configuration, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blConfiguration.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.Configuration, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blConfiguration.ChangeStatus(id, User.GetUserIdInt());
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
        #endregion
    }
}
