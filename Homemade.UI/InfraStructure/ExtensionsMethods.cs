using Homemade.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using Core = Microsoft.AspNetCore.Identity;


namespace Homemade.UI
{
    public static class ExtensionsMethods
    {
        public static int GetUserIdInt(this IPrincipal user) => int.TryParse(user.Identity.GetUserId(), out int id) ? id : 0;
        #region Session
        public static void SetObject(this ISession session , string key , object value)
        {
            session.SetString(key , JsonConvert.SerializeObject(value));
        }
        public static T GetObject<T>(this ISession session , string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
        #endregion
        public static string GetAppMainLink(this HttpRequest request) => request.Scheme + "://" + request.Host + "/";

        #region Cookie
        public static string GetLanguage(this IRequestCookieCollection cookie)
        {
            return !cookie.Keys.Contains("Language") ? "ar" : cookie["Language"];
        }
        public static bool IsArabic(this IRequestCookieCollection cookie)
        {
            return GetLanguage(cookie)?.Contains("ar") ?? false;
        }
        #endregion
    }
}
