using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homemade.UI.Controllers
{
    public class AccessDeniedController : Controller
    {
        // GET: AccessDenied
        public ActionResult Unauthorized()
        {
            if (User.Identity.IsAuthenticated)
            {
                    return View();
            }
            else
            {
                return LocalRedirect("/Identity/Account/Login");
            }
        }
    }
}