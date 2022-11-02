using Homemade.BLL;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Setting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class DeliverySettingController : Controller
    {
        #region Declarations
        private readonly BlDeliverySetting _blDeliverySetting;
        private readonly ResultMessage result;
        public DeliverySettingController(BlDeliverySetting blDeliverySetting, ResultMessage result)
        {
            _blDeliverySetting = blDeliverySetting;
            this.result = result;
        }
        #endregion
        #region View
        public IActionResult Index()
        {
            DeliverySettingViewModel deliverySettingViewModel = _blDeliverySetting.GetDeliverySettingViewModel();
            if (deliverySettingViewModel != null)
            {
                return View(deliverySettingViewModel);
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        #endregion
        #region Action
        [HttpPost]
        public JsonResult Index(DeliverySettingViewModel viewModel)
        {
            try
            {
                #region Update
                int UserId = User.GetUserIdInt();
                if (_blDeliverySetting.Update(viewModel, UserId))
                {
                    result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                    result.ResultType = ResultMessageType.success.ToString();
                    result.Status = true;
                    return Json(result);
                }
                else
                {
                    result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Status = false;
                    return Json(result);
                }
                #endregion

            }
            catch (System.Exception ex)
            {

                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                result.Status = false;
                return Json(result);
            }
        }
        #endregion
    }
}
