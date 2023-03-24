using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Homemade.Api.Authentication
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {

            if (filterContext != null)
            {
                Microsoft.Extensions.Primitives.StringValues Authorizations;
                filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out Authorizations);

                var _token = Authorizations.FirstOrDefault();

                if (_token != null)
                {
                    string Authorization = _token;
                    if (Authorization != null)
                    {
                        if (IsValidToken(Authorization))
                        {
                            filterContext.HttpContext.Response.Headers.Add("Authorization", Authorization);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                            return;
                        }
                        else
                        {
                            filterContext.HttpContext.Response.Headers.Add("Authorization", Authorization);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {

                                    message = "Invalid Token. Login again",
                                    status = false,
                                    isAuthorize = false

                                },
                            };
                        }

                    }

                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide Authorization";
                    filterContext.Result = new JsonResult("Please Provide Authorization")
                    {
                        Value = new
                        {
                            message = "Please Provide Authorization",
                            status = false,
                            isAuthorize = false
                        },
                    };
                }
            }
        }
        public bool IsValidToken(string Authorization)
        {
            var tokenString = Authorization;
          
            var jwtEncodedString = tokenString.Substring(7); // trim 'Bearer ' 
            try
            {
                var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
