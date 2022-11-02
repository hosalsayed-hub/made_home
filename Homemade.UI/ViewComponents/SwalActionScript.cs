using Homemade.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.ViewComponents
{
    [ViewComponent(Name = "SwalActionScript")]
    public class SwalActionScript : ViewComponent
    {
        public IViewComponentResult Invoke(Swal mdl)
        {
            return View("Index" , mdl);
        }
    }
}
