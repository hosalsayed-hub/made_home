using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Controllers
{
    public class MonitoringController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
