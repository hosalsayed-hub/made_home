using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.Areas.Employees
{
    [Area("Employees")]
    [Authorize]
    public class Controller : Homemade.UI.InfraStructure.Controller.Controller
    {

    }
}
