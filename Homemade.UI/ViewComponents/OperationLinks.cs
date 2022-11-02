using Microsoft.AspNetCore.Mvc;


namespace Homemade.UI.ViewComponents
{
    [ViewComponent(Name = "OperationLinks")]
    public class OperationLinks : ViewComponent
    {
        public IViewComponentResult Invoke() => View("Index");
    }
}
