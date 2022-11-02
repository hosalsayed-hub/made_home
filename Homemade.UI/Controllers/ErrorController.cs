using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
 
namespace Homemade.UI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("api/error")]
        public IActionResult LogError()
        {
            return BadRequest();

        }
    }
}
