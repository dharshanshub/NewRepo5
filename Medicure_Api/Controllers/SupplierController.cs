using DataAccessLib;
using Medicure_Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicure_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        Drug_Dal dd;
        Supplier_Dal sd;
        public SupplierController()
        {
            sd = new Supplier_Dal();
            dd = new Drug_Dal();
        }
        [HttpPost("NewSupplier")]
        public IActionResult NewSupplier(Supplier s)
        {
            sd.NewSupplier(s);
            return Ok(s);
        }

        [HttpPost("CreateDrug")]
        public IActionResult CreateDrug(Drug d)
        {
            dd.CreateDrug(d);
            return Ok(d);
        }
        [HttpPost("SupplierLogin")]
        public IActionResult SupplierLogin(Login l)
        {
            
            return Ok(sd.SupplierLogin(l.Username, l.Password));
        }

        [HttpGet("SupplierById")]
        public IActionResult SupplierById(int id)
        {
            return Ok(sd.SupplierById(id));
            
        }
        [HttpDelete("DeleteDrug")]
        public IActionResult DeleteDrug(int id)
        {
            dd.DeleteDrug(id);
            return Ok(id);

        }
        [HttpGet("GetAllDrug")]
        public IActionResult GetAllDrug()
        {
            return Ok(dd.GetAllDrug());
        }
        [HttpGet("DrugsToBeSuppiled")]
        public IActionResult DrugSupplier(int id)
        {
            return Ok(dd.DrugSupplier(id));
        }
        [HttpPost("SupplyDrugs")]
        public IActionResult SupplierDrugsbtn(Supplier_Log s)
        {
            dd.SupplierDrugsbtn(s);
            return Ok(s);

        }
        [HttpGet("DrugById")]
        public IActionResult DrugById(int id)
        {
           return Ok( dd.DrugById(id));
        }
    }
}
