using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.ViewComponents
{
    [ViewComponent(Name = "CompanyLinks")]
    public class CompanyLinks : ViewComponent
    {
        public IViewComponentResult Invoke() => View("Index");
    }
}
