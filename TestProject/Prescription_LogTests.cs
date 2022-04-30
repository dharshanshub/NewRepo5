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
    public class Prescription_LogTests
    {
        Prescription_Log_Dal pd = new Prescription_Log_Dal();
        [Test]
        public void newPrescription()
        {
            Prescription_Log p = new Prescription_Log();
            p.Appointment_ID = 2;
            p.Drug_Id = 1;
            p.Dosage = 5;

            pd.newPrescription(p);

        }
    }
}
