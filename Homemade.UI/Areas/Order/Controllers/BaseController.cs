using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.Areas.Order
{
    [Area("Order")]
    [Authorize]
    public class Controller : Homemade.UI.InfraStructure.Controller.Controller
    {

    }
}
