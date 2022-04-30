using Medicure_Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using ProjectsMVCApp;

namespace Medicure_Mvc.Controllers
{

    public class PatientController : Controller
    {
        private IConfiguration configuration;
        private static int _id;
        public PatientController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index(int id)
        {
            _id = id;
            var model = await this.GetResponseFromApi<Patient>(
                baseUri: configuration.GetConnectionString("PatientUri"),
                requestUrl: $"api/Patient/GetPatientDetailsByID?id={id}"

                );
            model.Id = id;
            if (_id == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(model);
        }


        public async Task<IActionResult> BookAppointment(int id)
        {
            ViewBag.Id = id;
            var l = await this.GetResponseFromApi<List<Physician>>(
                baseUri: configuration.GetConnectionString("PatientUri"),
                requestUrl: "/api/Patient/AllPhysician"
                );
            List<string> dl = new List<string>();
            foreach (var i in l)
            {
                string s = i.Id + "-" + i.Name + " " + i.Specialization;
                dl.Add(s);
            }
            ViewBag.ddl = dl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BookAppointment(IFormCollection form)
        {
            Appointment_Log ap = new Appointment_Log();
            try
            {
                TempData["error"] = null;
                ap.Appointment_ID = Convert.ToInt32(form["Appointment_ID"]);

                ap.Patient_Id = Convert.ToInt32(form["Patient_Id"]);


                ap.Physician_Id = Convert.ToInt32(form["Physician_Id"]);
                ap.illness = form["illness"];
                ap.Date_of_visit = form["Date_of_visit"];
            }
            catch
            {

                TempData["error"] = "I am from different action";
                return RedirectToAction("Index", new { @id = _id });
            }
            var model = await this.SendDataToApi<Appointment_Log>(
                baseUri: configuration.GetConnectionString("PatientUri"),
                requestUrl: "api/Patient/BookAppointment",
                ap
                );
            return RedirectToAction("Index", new { @id = _id });


        }
        public IActionResult NewPaitents()
        {
            var model = new Patient();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> NewPaitents(IFormCollection form)



        {
            Patient d = new Patient();

            d.Name = form["Name"];
            d.MobileNo = form["MobileNo"];
            d.DateOfReg = form["DateOfReg"];
            d.Username = form["Username"];
            d.Password = form["Password"];



            var model = await this.SendDataToApi<Patient>(
            baseUri: configuration.GetConnectionString("PatientUri"),
            requestUrl: $"api/Patient/NewPaitents",
            d
            );
            return RedirectToAction("Login", "Home");



        }

        public async Task<IActionResult> EditPatient(int id)
        {
            var model = await this.GetResponseFromApi<Patient>(
                baseUri: configuration.GetConnectionString("PatientUri"),
                requestUrl: $"api/Patient/GetPatientDetailsByID?id={id}"

                );
            model.Id = id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPatient(IFormCollection form)
        {
            Patient p = new Patient();
            p.Id = Convert.ToInt32(form["Id"]);
            p.Name = form["Name"];
            p.MobileNo = form["MobileNo"];
            p.Username = form["Username"];
            p.Password = form["Password"];
            var model = await this.SendDataToApi<Patient>(
                baseUri: configuration.GetConnectionString("PatientUri"),
                requestUrl: $"api/Patient/EditPatient",
                p

                );
            return RedirectToAction("Index", new { @id = _id });
        }
        public async Task<IActionResult> GetAllDrug(int id)
        {
            ViewBag.Id = id;
            var model = await this.GetResponseFromApi<List<Drug>>(
                baseUri: configuration.GetConnectionString("PatientUri"),
                requestUrl: $"api/Patient/ViewDrugs"
                );
            return View(model);
        }

        public async Task<IActionResult> ViewAppointmentHistory(int id)
        {
            ViewBag.Id = id;
            var model = await this.GetResponseFromApi<List<Appointment_Log>>(
                baseUri: configuration.GetConnectionString("PatientUri"),
                requestUrl: $"/api/Patient/ViewAppointmentHistory?id={id}"

                );
            return View(model);
        }
        public async Task<IActionResult> GetPhysicianDetailsByID(int id)
        {
            ViewBag.Id = _id;
            var model = await this.GetResponseFromApi<Physician>(
                baseUri: configuration.GetConnectionString("PatientUri"),
                requestUrl: $"/api/Patient/GetPhysicianDetailsByID?id={id}"

                );
            return View(model);
        }


        //Not needed
        public async Task<IActionResult> GetAllPatient()
        {
            var model = await this.GetResponseFromApi<IEnumerable<Patient>>(
                baseUri: configuration.GetConnectionString("PatientUri"),
                requestUrl: "/api/Patient/GetAllPatient"
                );

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            _id = 0;
            return RedirectToAction("Login","Home");
        }



    }
}
