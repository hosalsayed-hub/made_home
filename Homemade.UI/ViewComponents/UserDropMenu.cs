using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.ViewComponents
{
    [ViewComponent(Name = "UserDropMenu")]
    public class UserDropMenu : ViewComponent
    {
        public IViewComponentResult Invoke() => View("Index");
    }
}
