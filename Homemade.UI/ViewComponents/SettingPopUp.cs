using Homemade.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.ViewComponents
{
    [ViewComponent(Name = "SettingPopUp")]
    public class SettingPopUp : ViewComponent
    {
        public IViewComponentResult Invoke(SettingPopUpModel mdl)
        {
            return View("Index" , mdl);
        }
    }
}
