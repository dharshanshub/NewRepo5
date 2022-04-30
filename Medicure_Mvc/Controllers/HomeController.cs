using Medicure_Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using Medicure_Entity;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Medicure_Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            this.configuration = configuration;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //var model = await this.SendDataToApi<Login, AuthenticatedUser<int>>(
            //    baseUri: configuration.GetConnectionString("PatientUri"),
            //    requestUrl: $"api/login",
            //    new Login { Type = "", Username = "ram", Password = "ram" }
            //    );
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection form)
        {
            
                Login l = new Login();
                l.Type = form["Type"];
                l.Username = form["Username"];
                l.Password = form["Password"];



                if (l.Type == "Patient")
                {
                    var result = new Patient();
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(configuration.GetConnectionString("PatientUri"));

                        var response = await client.PostAsync(
                            requestUri: "/api/Patient/PatientLogin",
                            content: JsonContent.Create(
                                    inputValue: l,
                                    inputType: typeof(Login),
                                    mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"),
                                    options: new JsonSerializerOptions(JsonSerializerDefaults.Web)
                            ));
                        if (response.IsSuccessStatusCode)
                        {
                            result = JsonSerializer.Deserialize<Patient>(
                                await response.Content.ReadAsStringAsync(),
                                new JsonSerializerOptions(JsonSerializerDefaults.Web));

                        }
                        if (result.Id != 0)
                        {
                            return RedirectToAction(actionName: "Index", controllerName: "Patient", new { @id = result.Id });
                        }
                        else
                        {
                            return RedirectToAction("Login");
                        }
                    }
                }

                else if (l.Type == "Physician")
                {
                    var result = new Physician();
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(configuration.GetConnectionString("PhysicianUri"));

                        var response = await client.PostAsync(
                            requestUri: "/api/Physician/PhysicianLogin",
                            content: JsonContent.Create(
                                    inputValue: l,
                                    inputType: typeof(Login),
                                    mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"),
                                    options: new JsonSerializerOptions(JsonSerializerDefaults.Web)
                            ));
                        if (response.IsSuccessStatusCode)
                        {
                            result = JsonSerializer.Deserialize<Physician>(
                                await response.Content.ReadAsStringAsync(),
                                new JsonSerializerOptions(JsonSerializerDefaults.Web));

                        }
                        if (result.Id != 0)
                        {
                            return RedirectToAction(actionName: "Index", controllerName: "Physician", new { @id = result.Id });
                        }
                        else
                        {
                            return RedirectToAction("Login");
                        }
                    }
                }
                else if (l.Type == "Supplier")
                {
                    var result = new Supplier();
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(configuration.GetConnectionString("PhysicianUri"));

                        var response = await client.PostAsync(
                            requestUri: "/api/Supplier/SupplierLogin",
                            content: JsonContent.Create(
                                    inputValue: l,
                                    inputType: typeof(Login),
                                    mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"),
                                    options: new JsonSerializerOptions(JsonSerializerDefaults.Web)
                            ));
                        if (response.IsSuccessStatusCode)
                        {
                            result = JsonSerializer.Deserialize<Supplier>(
                                await response.Content.ReadAsStringAsync(),
                                new JsonSerializerOptions(JsonSerializerDefaults.Web));

                        }
                        if (result.SupplierId != 0)
                        {
                            return RedirectToAction(actionName: "Index", controllerName: "Supplier", new { @id = result.SupplierId });
                        }
                        else
                        {
                            return RedirectToAction("Login");
                        }
                    }
                }
                    else if (l.Type == "Admin")
                    {
                if (l.Username == "admin" && l.Password == "admin")
                {
                    return RedirectToAction(actionName: "Index", controllerName: "Admin");
                }
                else
                {
                    return RedirectToAction("Login");
                }


                    }
                else
                {
                    return RedirectToAction("Login");
                }
            }
        }
    }
        