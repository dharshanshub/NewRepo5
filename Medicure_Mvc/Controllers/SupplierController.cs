using Medicure_Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medicure_Mvc.Controllers
{
    public class SupplierController : Controller
    {
        private static int _id;
        IConfiguration configuration;

        public SupplierController(IConfiguration configuration)
        {
            
            
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index(int id)
        {
            _id =id;
            var model = await this.GetResponseFromApi<Supplier>(
                baseUri: configuration.GetConnectionString("SupplierUri"),
                requestUrl: $"api/Supplier/SupplierById?id={id}"

                );
            //if (_id == 0)
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            return View(model);
        }
        public async Task<IActionResult> DrugsToBeSuppiled(int id)
        {
            
            ViewBag.Sid = id;
            var model = await this.GetResponseFromApi<List<Drug>>(
                baseUri: configuration.GetConnectionString("SupplierUri"),
                requestUrl: $"api/Supplier/DrugsToBeSuppiled?id={id}"

                );
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SupplierDrugsbtn(int id)

        {


            var s = await this.GetResponseFromApi<Drug>(
                baseUri: configuration.GetConnectionString("SupplierUri"),
                requestUrl: $"api/Supplier/DrugById?id={id}"
                );

            Supplier_Log sl = new Supplier_Log();
            sl.Date = DateTime.Now.ToString();
            sl.Drug_id = s.Id;
            sl.Qty = s.Required_Qty;
            sl.Supplier_Id = s.Supplier_Id;

            var m = await this.SendDataToApi<Supplier_Log>(
                baseUri: configuration.GetConnectionString("SupplierUri"),
                requestUrl: $"/api/Supplier/SupplyDrugs?id={id}",
                sl
                );
            return RedirectToAction("Index", new { @id = _id });
        }

        public IActionResult CreateDrug(int id)
        {
            
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDrug(IFormCollection form)

        {
            Drug d = new Drug();
            d.Id =Convert.ToInt32( form["Id"]);
            d.Instock_Qty = Convert.ToInt32(form["Instock_Qty"]);
            d.Name = form["Name"];
            d.Price = Convert.ToInt32(form["Price"]);
            d.Supplier_Id = Convert.ToInt32(form["Supplier_Id"]);

            var model = await this.SendDataToApi<Drug>(
                baseUri: configuration.GetConnectionString("SupplierUri"),
                requestUrl: $"api/Supplier/CreateDrug",
                d
               );
            return RedirectToAction("Index","Supplier", new { @id = _id});

        }
        public async Task<IActionResult> Logout()
        {
            _id = 0;
            return RedirectToAction("Login", "Home");
        }

    }
}
