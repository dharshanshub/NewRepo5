using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLib;
using Medicure_Entity;
using NUnit.Framework;

namespace TestProject
{
    public class Supplier_LogTests
    {
        Supplier_Log_Dal sld = new Supplier_Log_Dal();
        
        public void AddLog()
        {
            Supplier_Log s = new Supplier_Log();
            s.Supplier_Id = 1;
            s.Physician_ID = 1;
            s.Drug_id = 1;
            s.Qty = 5;
            sld.AddLog(s);
            var er = sld.Supplier_Log_Details(1).FirstOrDefault().Drug_id;
            var ar = sld.Physician_Log_Details(1).FirstOrDefault().Drug_id;

            Assert.AreEqual(er, ar);
        }
        
    }

}