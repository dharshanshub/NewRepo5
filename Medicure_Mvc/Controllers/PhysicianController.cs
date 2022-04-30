using Medicure_Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Medicure_Mvc.Controllers
{
    public class PhysicianController : Controller
    {
        private IConfiguration configuration;
        private static int _id = 0;
        public PhysicianController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index(int id)
        {
            _id = id;
            var model = await this.GetResponseFromApi<Physician>(
              baseUri: configuration.GetConnectionString("PhysicianUri"),
                requestUrl: $"api/Physician/GetPhysicianDetailsByID?id={id}"
                );
            if (_id == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(model);
        }
        public async Task<IActionResult> ViewAppointments(int id)
        {
            var model = await this.GetResponseFromApi<List<Appointment_Log>>(
                  baseUri: configuration.GetConnectionString("PhysicianUri"),
                requestUrl: $"api/Physician/ViewAppointments?id={id}"
                );
            return View(model);
        }
        public async Task< IActionResult> AddPrescription(int id, int pid)
        {
            ViewBag.Id = id;
            ViewBag.pid = pid;
            var model = await this.GetResponseFromApi<List<Drug>>(
               baseUri: configuration.GetConnectionString("PhysicianUri"),
               requestUrl: $"api/Physician/GetallDrugs"
               );
            List<string> dl = new List<string>(); 
            foreach (var i in model)
            {
                string s = i.Id + "-" + i.Name + " " + i.Price;
                dl.Add(s);
            }
            ViewBag.ddl = dl;
            return View();

        }
        [HttpPost]
        public IActionResult AddPrescription(IFormCollection form)
        {
            TempData["error"] = null;
            Prescription_Log p = new Prescription_Log();
            try
            {
                p.Appointment_ID = Convert.ToInt32(form["Appointment_ID"]);
                p.Drug_Id = Convert.ToInt32(form["Drug_Id"]);
                p.Dosage = Convert.ToInt32(form["Dosage"]);
            }
            catch(Exception ex)
            {
                TempData["error"] = "I am from different action";
                return RedirectToAction("Index", new { @id = _id});
                
            }
            var model = this.SendDataToApi<Prescription_Log>(
                baseUri: configuration.GetConnectionString("PhysicianUri"),
                requestUrl: $"api/Physician/AddPrescription",
                p
                );
            return RedirectToAction("Index", new { @id = _id });
        }
        public async Task<IActionResult> GetPatientDetailsByID(int id)
        {
            var model = await this.GetResponseFromApi<Patient>(
                baseUri: configuration.GetConnectionString("PhysicianUri"),
                requestUrl: $"api/Physician/GetPatientDetailsByID?id={id}"
                );
            return View(model);

        }
        public async Task<IActionResult> GetallDrugs()
        {
            var model = await this.GetResponseFromApi<List<Drug>>(
                baseUri: configuration.GetConnectionString("PhysicianUri"),
                requestUrl: $"api/Physician/GetallDrugs"
                );
            return View(model);
        }
        public async Task<IActionResult> AppointmentHistory(int id)
        {
            var model = await this.GetResponseFromApi<List<Appointment_Log>>(
                baseUri: configuration.GetConnectionString("PhysicianUri"),
                requestUrl: $"api/Physician/AppointmentHistory?id={id}"
                );
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            _id = 0;
            return RedirectToAction("Login", "Home");
        }

    }
}
