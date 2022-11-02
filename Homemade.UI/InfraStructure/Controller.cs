using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace Homemade.UI.InfraStructure.Controller
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-EG");
            string LangId = Request.Cookies.ContainsKey("Language") ? Request.Cookies["Language"] : "ar";
            Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(LangId)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
               );

            base.OnActionExecuting(context);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> GetGoogleMapScript()
        {

            string k = "";
            try
            {
                IConfiguration configuration = HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
                string apiKey = configuration.GetValue<string>("GoogleApiKey");

                HttpClient httpClient = new HttpClient();
                k = await httpClient.GetStringAsync($"https://maps.googleapis.com/maps/api/js?key={apiKey}&callback=initMap&libraries=places&v=weekly");

            }
            catch { }

            return Json(k);
        }
    }
}
