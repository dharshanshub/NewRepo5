using DataAccessLib;
using Medicure_Api.Authentication;
using Medicure_Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Medicure_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        Paitent_Dal pd;
        Physician_Dal phd;
        Supplier_Dal sd;
        public LoginController(IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
            pd = new Paitent_Dal();
            phd = new Physician_Dal();
            sd = new Supplier_Dal();

        }
        [HttpPost("AuthenticatePatient")]
        public IActionResult AuthenticatePatient(Login model)
        {
            var status = false;
            var DAO = pd.PatientLogin(model.Username, model.Password);
            if (DAO == null)
            {
                status = false;
            }
            else
            {
                status = true;
            }
           
            
            if (status == false)
                return Unauthorized();

            //Create the token as authentication has succeeded
            JwtSecurityTokenHandler tokenHandler = new();
            var key = System.Text.Encoding.UTF8.GetBytes(_appSettings.AppSecretKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new System.Security.Claims.Claim("Username", model.Username),
                        new System.Security.Claims.Claim("RoleName", "Patient"),
                    }),
                Expires = System.DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var authresponse = new AuthenticatedUser<int>(DAO.Id, model.Username, "Patient", token);
            return Ok(authresponse);
        }
        [HttpPost("AuthenticatePhysician")]
        public IActionResult AuthenticatePhysician(Login model)
        {
            var status = false;
            var DAO = phd.PhysicianLogin(model.Username, model.Password);

            if (DAO == null)
            {
                status = false;
            }
            else
            {
                status = true;
            }


            if (status == false)
                return Unauthorized();

            //Create the token as authentication has succeeded
            JwtSecurityTokenHandler tokenHandler = new();
            var key = System.Text.Encoding.UTF8.GetBytes(_appSettings.AppSecretKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new System.Security.Claims.Claim("Username", model.Username),
                        new System.Security.Claims.Claim("RoleName", "Physician"),
                    }),
                Expires = System.DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var authresponse = new AuthenticatedUser<int>(DAO.Id, model.Username, "Physician", token);
            return Ok(authresponse);
        }
        [HttpPost("AuthenticateSupplier")]
        public IActionResult AuthenticateSupplier(Login model)
        {
            var status = false;
            var DAO = sd.SupplierLogin(model.Username,model.Password);

            if (DAO == null)
            {
                status = false;
            }
            else
            {
                status = true;
            }


            if (status == false)
                return Unauthorized();

            //Create the token as authentication has succeeded
            JwtSecurityTokenHandler tokenHandler = new();
            var key = System.Text.Encoding.UTF8.GetBytes(_appSettings.AppSecretKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new System.Security.Claims.Claim("Username", model.Username),
                        new System.Security.Claims.Claim("RoleName", "Supplier"),
                    }),
                Expires = System.DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    key: new SymmetricSecurityKey(key),
                    algorithm: SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            var authresponse = new AuthenticatedUser<int>(DAO.SupplierId, model.Username, "Supplier", token);
            return Ok(authresponse);
        }
    }
}
