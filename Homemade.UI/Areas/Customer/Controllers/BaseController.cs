using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.Areas.Customer
{
    [Area("Customer")]
    [Authorize]
    public class Controller : Homemade.UI.InfraStructure.Controller.Controller
    {

    }
}
