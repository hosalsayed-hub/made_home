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
    public class SliderController : Controller
    {
        private readonly BlSlider blSlider;
        private readonly ResultMessage result;
        private readonly BLGeneral bLGeneral;
        private readonly IConfiguration _configuration;

        public SliderController(BlSlider blSlider, ResultMessage result, IConfiguration configuration, BLGeneral bLGeneral)
        {
            this.bLGeneral = bLGeneral;
            this.blSlider = blSlider;
            this.result = result;
            _configuration = configuration;
        }
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Sliders, (int)ActionEnum.View)]
        public IActionResult Index() =>


            View(new SliderViewModel());
        #endregion
        #region Actions

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
            string MainPathView = _configuration["SliderImageView"];
            #endregion
            var data = blSlider.GetAlSliders().Select(p => new
            {
                id = p.SliderID,
                nameAr = !string.IsNullOrEmpty(p.NameAR) ? p.NameAR : string.Empty,
                nameEn = !string.IsNullOrEmpty(p.NameEN) ? p.NameEN : string.Empty,
                descAR = !string.IsNullOrEmpty(p.DescAr) ? p.DescAr : string.Empty,
                descEN = !string.IsNullOrEmpty(p.DescEN) ? p.DescEN : string.Empty,
                image = !string.IsNullOrEmpty(p.Image) ? (MainPathView + p.Image) : "/Images/noImage.png",
                typeSlider = p.SliderTypeId,
                p.IsEnable,
                name = Utility.CurrentLanguageCode == "ar" ? (!string.IsNullOrEmpty(p.NameAR) ? p.NameAR : string.Empty) : (!string.IsNullOrEmpty(p.NameEN) ? p.NameEN : string.Empty),
                IsEnableString = Utility.CurrentLanguageCode == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
                typeSliderStr = Utility.CurrentLanguageCode == "ar" ? (p.SliderTypeId == (int)SliderTypeEnum.ADV ? "إعلان" : "بانر") : (p.SliderTypeId == (int)SliderTypeEnum.ADV ? "Advertisement" : "Banner"),
                place = Utility.CurrentLanguageCode == "ar" ? (p.DisplayIn == (int)DisplayInEnum.App ? "التطبيق" : "الموقع") : (p.DisplayIn == (int)DisplayInEnum.App ? "Application" : "Web Site"),
                displayInId = p.DisplayIn
            }).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.nameAr.ToLower().Contains(sea.ToLower()) || x.nameEn.ToLower().Contains(sea.ToLower())).ToList();
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
        [CustomeAuthoriz((int)ControllerEnum.Sliders, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(SliderViewModel model)
        {
            #region SaveData
            int SliderID = 0;
            string OperationType = "add";

            bool IsSuccess = false;
            if (model.SliderID == 0)
            {
                var Image = "";
                if (!string.IsNullOrEmpty(model.Image))
                {
                    string FileName = Guid.NewGuid() + ".png";
                    Image = FileName;
                    string MainPath = _configuration["SliderImageSave"];
                    bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                    {
                        base64 = model.Image,
                        key = "",
                        fileName = FileName,
                        mainPath = MainPath
                    });
                    model.Image = Image;
                }
                #region AddSlider
                model.CreateDate = bLGeneral.GetDateTimeNow();
                model.UserId = User.GetUserIdInt();
                if (string.IsNullOrWhiteSpace(model.SliderNameAR))
                {
                    model.SliderNameAR = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(model.SliderNameEN))
                {
                    model.SliderNameEN = string.Empty;
                }
                try
                {
                    IsSuccess = blSlider.Insert(model, out SliderID);
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
                var Image = "";
                if (!string.IsNullOrEmpty(model.Image))
                {
                    string FileName = Guid.NewGuid() + ".png";
                    Image = FileName;
                    string MainPath = _configuration["SliderImageSave"];
                    bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                    {
                        base64 = model.Image,
                        key = "",
                        fileName = FileName,
                        mainPath = MainPath
                    });
                    model.Image = Image;
                }
                #region UpdateSlider
                model.UpdateDate = bLGeneral.GetDateTimeNow();
                model.UserUpdate = User.GetUserIdInt();
                if (string.IsNullOrWhiteSpace(model.SliderNameAR))
                {
                    model.SliderNameAR = string.Empty;
                }
                if (string.IsNullOrWhiteSpace(model.SliderNameEN))
                {
                    model.SliderNameEN = string.Empty;
                }
                try
                {
                    IsSuccess = blSlider.Update(model);
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
            return Json(new { result, model.SliderNameAR, model.SliderNameEN, SliderID, OperationType });
        }
        [CustomeAuthoriz((int)ControllerEnum.Sliders, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blSlider.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.Sliders, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blSlider.ChangeStatus(id, User.GetUserIdInt());
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
        public JsonResult GetDisplayIns()
        {
            var DisplayIns = new List<object>() {
                new { DisplayInId = BLL.DisplayIn.Application , DisplayInName = Resources.Homemade.DisplayInEnumApplication} ,
                new { DisplayInId = BLL.DisplayIn.WebSite , DisplayInName = Resources.Homemade.DisplayInEnumWebSite}}.ToList();
            return Json(DisplayIns);
        }
    }
}
