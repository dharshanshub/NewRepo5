using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicure_Api.Authentication
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next; //Helps in triggering the next item in the Request pipeline.
        private readonly AppSettings _appSettings;
        //Use DAO/DataAccess/DAL objects to authenticate the user.
        //private LoginDAL loginDal;
        public JwtMiddleware(
        RequestDelegate next,
        IOptions<AppSettings> settings)
        {
            _next = next;
            _appSettings = settings.Value;
        }
        public async  Task Invoke(
        HttpContext context
        )
        {
            //check whether the context.Request contains the Authorization Header.
            //If it exists, get the header value, split it on space character and extract the last part of
            // the array. Split() returns an array of strings.
            //?. => NUll Conditional operator
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();
            if (token is not null)
            {
                if (token != "Token" && token != "Bearer")
                {


                    try
                    {
                        //Create a SecurityToken Handler
                        JwtSecurityTokenHandler tokenHandler = new();
                        var key = Encoding.UTF8.GetBytes(_appSettings.AppSecretKey);
                        tokenHandler.ValidateToken(
                        token,
                        new TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.Zero
                        }, out SecurityToken validatedToken);
                        var jwtToken = validatedToken as JwtSecurityToken;

                        var username = jwtToken.Claims.First(c => c.Type == "Username").Value;
                        context.Items["Username"] = username;

                        var Rolename = jwtToken.Claims.First(c => c.Type == "RoleName").Value;
                        context.Items["RoleName"] = Rolename;

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            //else nothing to be done by the middleware.
            //The context.Items will be null and the AuthorizationAttribute will return UnauthorizedResult.
            await _next(context);
        }
    }
}