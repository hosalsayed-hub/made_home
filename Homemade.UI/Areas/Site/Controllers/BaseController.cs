using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.Areas.Site
{
    [Area("Site")]
    [Authorize]
    public class Controller : Homemade.UI.InfraStructure.Controller.Controller
    {

    }
}
