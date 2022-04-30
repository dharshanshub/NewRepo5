using DataAccessLib;
using Medicure_Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicure_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PhysicianController : ControllerBase
    {
        Appointment_Log_Dal ald;
        Drug_Dal dd;
        Physician_Dal pd;
        Paitent_Dal pad;
        Prescription_Log_Dal pld;
        Supplier_Log_Dal sld;
        public PhysicianController()
        {
            ald = new Appointment_Log_Dal();
            dd = new Drug_Dal();
            pd = new Physician_Dal();
            pad = new Paitent_Dal();
            pld = new Prescription_Log_Dal();
            sld = new Supplier_Log_Dal();
        }
        [HttpPost("PhysicianLogin")]
        public IActionResult PhysicianLogin(Login l)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                return Ok( pd.PhysicianLogin(l.Username, l.Password));

            }
        }
        [HttpPost("NewPhysician")]
        public IActionResult NewPhysician(Physician p)
        {
            pd.NewPhysician(p);
            return Ok(p);
        }
        [HttpGet("GetPhysicianDetailsByID")]
        public IActionResult GetPhysicianById(int id)
        {
            return Ok(pd.PhysicianById(id));
        }
        [HttpGet("GetallDrugs")]
        public IActionResult GetallDrugs()
        {
            return Ok(dd.GetAllDrug());
        }
        [HttpGet("ViewAppointments")]
        public IActionResult View_Appointment(int id)
        {
            return Ok( pd.View_Appointment(id));
        }
        [HttpGet("GetPatientDetailsByID")]
        public IActionResult GetPaitentById(int id)
        {
            return Ok(pad.GetPaitentById(id));
        }
        [HttpPost("AddPrescription")]
        public IActionResult newPrescription(Prescription_Log pl)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            {
                pld.newPrescription(pl);
                return Ok(pl);
            }
        }
        [HttpGet ("DrugHistory")]
        public IActionResult Physician_Log_Details(int id)
        {
            return Ok(sld.Physician_Log_Details(id));
        }
        [HttpGet("AppointmentHistory")]
        public IActionResult View_Appointment_History(int id)
        {
            return Ok(pd.View_Appointment_History(id));
        }
        [HttpDelete("DeleteAppointment")]
        public IActionResult Delete_Appointment(int id)
        {
            ald.Delete_Appointment(id);
            return Ok(id);
        }

    }
}
