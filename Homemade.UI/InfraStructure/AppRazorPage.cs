using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;
using System.Threading;

namespace Homemade.UI
{
    public abstract class AppRazorPage<Tmodel> : RazorPage<Tmodel>
    {
        private IStringLocalizerFactory _stringLocalizerFactory;

        [RazorInject]
        public IStringLocalizerFactory StringLocalizerFactory
        {
            get => _stringLocalizerFactory;
            set
            {
                _stringLocalizerFactory = value;
                string LangId = ViewContext.HttpContext.Request.Cookies.ContainsKey("Language") ? ViewContext.HttpContext.Request.Cookies["Language"] : "ar";
                
                InfraStructure.Utility.CurrentLanguageCode = LangId.Contains("en") ? "en" : "ar";

                ViewContext.HttpContext.Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(LangId)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
               );

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangId);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangId);

            }
        }

    }
}
