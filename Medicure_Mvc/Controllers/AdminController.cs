using Medicure_Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Medicure_Mvc.Controllers
{
    public class AdminController : Controller
    {
        private IConfiguration configuration;

        public AdminController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateNewPhysician()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewPhysician(IFormCollection form)
        {
            Physician p = new Physician();
            p.Name = form["Name"];
            p.Specialization = form["Specialization"];
            p.Username = form["Username"];
            p.Password = form["Password"];

            var model = await this.SendDataToApi<Physician>(
            baseUri: configuration.GetConnectionString("PatientUri"),
            requestUrl: $"/api/Physician/NewPhysician",
            p
               );
            return RedirectToAction("Index");
        }
        public IActionResult NewSupplier()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewSupplier(IFormCollection form)
        {
            Supplier s = new Supplier();
            s.SupplierId = Convert.ToInt32(form["SupplierId"]);
            s.Username = form["Username"];
            s.Password = form["Username"];
            var model = await this.SendDataToApi<Supplier>(
            baseUri: configuration.GetConnectionString("PatientUri"),
            requestUrl: $"/api/Supplier/NewSupplier",
            s
            );
            return RedirectToAction("Index");
        }



    }
}
