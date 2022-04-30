using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLib;
using NUnit.Framework;

namespace TestProject
{
    public class SupplierTests
    {
        Supplier_Dal sd = new Supplier_Dal();
        [Test]
        public void SupplierLogin()
        {
            var item = sd.SupplierLogin("Vinodh", "Vinodh");
            var er = item.SupplierId;
            var ar = 1;
            Assert.AreEqual(er, ar);

        }

    }
}
