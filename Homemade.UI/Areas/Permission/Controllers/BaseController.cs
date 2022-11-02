using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.Areas.Permission
{
    [Area("Permission")]
    [Authorize]
    public class Controller : Homemade.UI.InfraStructure.Controller.Controller
    {

    }
}
