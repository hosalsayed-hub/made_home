using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.ViewComponents
{
    [ViewComponent(Name = "AppLinks")]
    public class AppLinks : ViewComponent
    {
        public IViewComponentResult Invoke() => View("Index");
    }
}
