using Homemade.BLL;
using Homemade.DAL.Contract;
using Homemade.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class CustomeAuthoriz : Attribute, IAuthorizationFilter
    {
        private bool authenticated;
        private bool authorize;
 
        private readonly int allowedrController;
        private readonly int allowedAction;


        public CustomeAuthoriz(int controllerId, int actionId)
        {
            this.allowedrController = controllerId;
            this.allowedAction = actionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            BLPermission permissionObj = new BLPermission(null);
            authorize = false;
            authenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (authenticated)
            {
                IConfiguration Configuration = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
                var userID = context.HttpContext.User.Identity.GetUserId();
                authorize = permissionObj.GetPermissionForAction(allowedrController , allowedAction , userID , Configuration);
            }
            if (authorize)
            {
                return;
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
