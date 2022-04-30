using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLib;
using NUnit.Framework;

namespace TestProject
{
    public class PhysicianTests
    {
        
        Physician_Dal pd = new Physician_Dal();
        [Test]
        public void PhysicianLogin()
        {
            var er = pd.PhysicianLogin("Sachin", "Sachin").Name;
            var ar = "Sachin";
            Assert.AreEqual(er, ar);
        }
       
        public void View_Appointment()
        {
            var i =pd.View_Appointment(1);
            var er = i.FirstOrDefault().Patient_Id;
            var ar = 1;
            Assert.AreEqual(er, ar);

        }

    }
}
