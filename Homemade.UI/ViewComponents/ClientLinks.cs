using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.ViewComponents
{
    [ViewComponent(Name = "ClientLinks")]
    public class ClientLinks : ViewComponent
    {
        public IViewComponentResult Invoke() => View("Index");
    }
}
