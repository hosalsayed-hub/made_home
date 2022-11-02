using Homemade.BLL;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Setting;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class CompanyWorkingHoursController : Controller
    {
        #region Declarations
        private readonly BlCompanyWorkingHours blCompanyWorkingHours;
        private readonly ResultMessage result;
        public CompanyWorkingHoursController(BlCompanyWorkingHours blCompanyWorkingHours, ResultMessage result)
        {
            this.blCompanyWorkingHours = blCompanyWorkingHours;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Configuration, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            var model = blCompanyWorkingHours.GetCompanyWorkingHoursViewModel();
            if(model == null)
            {
                model = new CompanyWorkingHoursViewModel();
            }
            return View(model);
        }
        #endregion
        #region Actions
        [CustomeAuthoriz((int)ControllerEnum.Configuration, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(CompanyWorkingHoursViewModel viewModel)
        {
            #region SaveData
            var IsSuccess = false;
            try
            {
                viewModel.UserId = User.GetUserIdInt();
                viewModel.DaysWork = String.Join(",", viewModel.ListDaysWork);
                if (viewModel.ListDaysVac != null && viewModel.ListDaysVac.Any())
                {
                    viewModel.DaysVac = String.Join(",", viewModel.ListDaysVac.Where(x => !viewModel.ListDaysWork.Any(y => y == x)));
                }
                IsSuccess = blCompanyWorkingHours.Update(viewModel);
            }
            catch (System.Exception ex)
            {
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
            }
            #endregion
            if (IsSuccess)
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
            return Json(result);
        }
        #endregion
    }
}
