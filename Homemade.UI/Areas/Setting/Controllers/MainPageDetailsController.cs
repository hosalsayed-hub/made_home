using Homemade.BLL.Setting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;
using Microsoft.Extensions.Configuration;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class MainPageDetailsController : Controller
    {
        #region Declarations
        private readonly BlMainPageDetails blMainPageDetails;
        private readonly BlMainPage _BlMainPage;
        private readonly BlCity _BlCity;
        private readonly ResultMessage result;
        private readonly BLGeneral _bLGeneral;
        private readonly IConfiguration _configuration;
        public MainPageDetailsController(BlMainPageDetails blMainPageDetails, ResultMessage result, BLGeneral bLGeneral, BlCity blCity, BlMainPage blMainPage, IConfiguration configuration)
        {
            _BlCity = blCity;
            _BlMainPage = blMainPage;
            _bLGeneral = bLGeneral;
            _configuration = configuration;
            this.blMainPageDetails = blMainPageDetails;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.MainPages, (int)ActionEnum.View)]
        public IActionResult Index(Guid Id)
        {
            var MainPage = _BlMainPage.GetByGuidId(Id);
            return View(new MainPageDetailsViewModel() { MainPageGuid = Id, MainPageID = MainPage.MainPageID, MainPageName = Utility.CurrentLanguageCode == "ar" ? MainPage.NameAr : MainPage.NameEn });
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.MainPages, (int)ActionEnum.View)]
        public IActionResult WhoWeAre(Guid Id)
        {
            var MainPage = _BlMainPage.GetByGuidId(Id);
            var MainPageDetail = blMainPageDetails.GetAllMainPageDetailssByListMainPage(new string[] { MainPage.MainPageID.ToString() });
            if (MainPageDetail.Count() > 0)
            {
                var WhoWeArePageDetail = new MainPageDetailsViewModel();
                WhoWeArePageDetail.MainPageName = Utility.CurrentLanguageCode == "ar" ? MainPage.NameAr : MainPage.NameEn;
                WhoWeArePageDetail.TitleAr = MainPageDetail.FirstOrDefault().TitleAr;
                WhoWeArePageDetail.TitleEn = MainPageDetail.FirstOrDefault().TitleEn;
                WhoWeArePageDetail.DescAr = MainPageDetail.FirstOrDefault().DescAr;
                WhoWeArePageDetail.DescEn = MainPageDetail.FirstOrDefault().DescEn;
                WhoWeArePageDetail.VedioLink = MainPageDetail.FirstOrDefault().VedioLink;
                WhoWeArePageDetail.Image = _configuration["AboutImageView"] + MainPageDetail.FirstOrDefault().Image;
                WhoWeArePageDetail.MainPageDetailsID = MainPageDetail.FirstOrDefault().MainPageDetailsID;
                WhoWeArePageDetail.MainPageID = MainPageDetail.FirstOrDefault().MainPageID;
                WhoWeArePageDetail.IdeaDescAr = MainPageDetail.FirstOrDefault().IdeaDescAr;
                WhoWeArePageDetail.IdeaTitleEn = MainPageDetail.FirstOrDefault().IdeaTitleEn;
                WhoWeArePageDetail.IdeaTitleAr = MainPageDetail.FirstOrDefault().IdeaTitleAr;
                WhoWeArePageDetail.IdeaDescAr = MainPageDetail.FirstOrDefault().IdeaDescAr;
                WhoWeArePageDetail.IdeaDescEn = MainPageDetail.FirstOrDefault().IdeaDescEn;
                WhoWeArePageDetail.HomeDescAr = MainPageDetail.FirstOrDefault().HomeDescAr;
                WhoWeArePageDetail.HomeDescEn = MainPageDetail.FirstOrDefault().HomeDescEn;
                WhoWeArePageDetail.HomeTitleAr = MainPageDetail.FirstOrDefault().HomeTitleAr;
                WhoWeArePageDetail.HomeTitleEn = MainPageDetail.FirstOrDefault().HomeTitleEn;
                return View(WhoWeArePageDetail);
            }
            else
            {
                return View(new MainPageDetailsViewModel() { MainPageGuid = Id, MainPageID = MainPage.MainPageID, MainPageName = Utility.CurrentLanguageCode == "ar" ? MainPage.NameAr : MainPage.NameEn });
            }
        }

        [CustomeAuthoriz((int)ControllerEnum.MainPages, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult WhoWeAre(MainPageDetailsViewModel model)
        {
            #region Vaildation
            if (string.IsNullOrWhiteSpace(model.TitleAr))
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.IBANnumberRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(model.TitleEn))
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.AccountNumberRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            #endregion
            #region SaveData
            int MainPageDetailsId = 0;
            string OperationType = "add";

            bool IsSuccess = false;
            if (model.MainPageDetailsID == 0)
            {
                #region AddCity
                model.CreateDate = _bLGeneral.GetDateTimeNow();
                model.UserId = User.GetUserIdInt();
                try
                {
                    var Image = "";
                    if (!string.IsNullOrEmpty(model.Image))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Image = FileName;
                        string MainPath = _configuration["AboutImageSave"];
                        _bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.Image,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.Image = Image;
                    }

                    IsSuccess = blMainPageDetails.Insert(model, out MainPageDetailsId);
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
                model.UpdateDate = _bLGeneral.GetDateTimeNow();
                model.UserUpdate = User.GetUserIdInt();
                try
                {
                    var Image = "";
                    if (!string.IsNullOrEmpty(model.Image))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Image = FileName;
                        string MainPath = _configuration["AboutImageSave"];
                        _bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.Image,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        model.Image = Image;
                    }
                    IsSuccess = blMainPageDetails.Update(model);
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
            return Json(new { result, MainPageDetailsId, OperationType });
        }
        #endregion
        #region Actions

        /// <summary>
        /// عرض التيبل
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoadTable(int mainPageId)
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

            var data = blMainPageDetails.GetMainPageDetailstaghelper(Utility.CurrentLanguageCode, mainPageId);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.TitleAr.ToLower().Contains(sea.ToLower()) || x.TitleEn.ToLower().Contains(sea.ToLower())).ToList();
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
        [CustomeAuthoriz((int)ControllerEnum.MainPages, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(MainPageDetailsViewModel model)
        {
            try
            {
                #region Vaildation
                if (string.IsNullOrWhiteSpace(model.TitleAr))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.TitleEn))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (blMainPageDetails.IsExist(model.MainPageDetailsID, model.TitleAr, Language.Arabic))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameIsExists;
                    return Json(new { result = result });
                }
                if (model.MainPageID == 0)
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.MainPageRequired;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }

                if (blMainPageDetails.IsExist(model.MainPageDetailsID, model.TitleEn, Language.English))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameEnIsExists;
                    return Json(new { result = result });

                }
                #endregion
                #region SaveData
                int MainPageDetailsID = 0;
                string OperationType = "add";

                bool IsSuccess;
                if (model.MainPageDetailsID == 0)
                {
                    #region AddMainPageDetails
                    model.CreateDate = _bLGeneral.GetDateTimeNow();
                    model.UserId = User.GetUserIdInt();
                    IsSuccess = blMainPageDetails.Insert(model, out MainPageDetailsID);
                    #endregion
                }
                else
                {
                    #region UpdateMainPageDetails
                    model.UpdateDate = _bLGeneral.GetDateTimeNow();
                    model.UserUpdate = User.GetUserIdInt();
                    IsSuccess = blMainPageDetails.Update(model);
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
                return Json(new { result, model.TitleAr, model.TitleEn, MainPageDetailsID, OperationType });
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                return Json(result);
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.MainPages, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blMainPageDetails.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.MainPages, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blMainPageDetails.ChangeStatus(id, User.GetUserIdInt());
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
        #region Helper
        public JsonResult GetAllMainPageDetails()
        {
            var obj = blMainPageDetails.GetAllMainPageDetails().Select(p => new { p.MainPageDetailsID, MainPageDetailsName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.TitleAr : p.TitleEn }).ToList();
            return Json(obj);
        }

        public JsonResult GetAllCities()
        {
            var obj = _BlCity.GetCities().Select(p => new { p.CityID, CityName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.CityNameAR : p.CityNameEN }).ToList();
            return Json(obj);
        }
        #endregion
    }
}
