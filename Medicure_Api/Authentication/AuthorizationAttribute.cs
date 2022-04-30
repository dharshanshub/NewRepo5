using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Medicure_Api.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : System.Attribute, IAuthorizationFilter
    {
        string _ReqRole;
        public AuthorizationAttribute( string reqRole)
        {
            _ReqRole = reqRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var Username = context.HttpContext?.Items["Username"]?.ToString();
            var Rolename = context.HttpContext?.Items["RoleName"]?.ToString();
            //check whether the username exists. If it does not exist, then return Unauthorized Result
            //to the user.
            //if (string.IsNullOrEmpty(Username))
            if (_ReqRole!=Rolename)
            {
                context.Result = new JsonResult(
                new{message = "Unauthorized. Please contact your system administrator for access."}
                )
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}


