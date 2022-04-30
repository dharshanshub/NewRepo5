using DataAccessLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TestProject
{
    public class DrugTests
    {
        Drug_Dal d = new Drug_Dal();
        [Test]
        public void GetAllDrug()
        {
           var ex= d.GetAllDrug().FirstOrDefault().Id;
            var ar = 1;

        }

    }
}
