using DataAccessLib;
using Medicure_Entity;
using NUnit.Framework;
using System.Linq;

namespace TestProject
{
    public class PaitentTests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void GetPaitentById()
        {
            Paitent_Dal p = new Paitent_Dal();

            var er = p.GetPaitentById(1).Name;
            var ar = "Raj";

            Assert.AreEqual(er,ar);
        }
        [Test]
        public void NewPaitents()
        {
            Paitent_Dal pd = new Paitent_Dal();
            var er = pd.GetAllPatient().Count();

            Patient p = new Patient();
            p.Name = "ram";
            p.MobileNo = "95450743";
            p.Username = "ram";
            p.Password = "ram";

            pd.NewPaitents(p);
            var ar = pd.GetAllPatient().Count();
            Assert.AreEqual(er+1, ar);


        }
        [Test]
        public void EditPatient()
        {
            Paitent_Dal pd = new Paitent_Dal();

            Patient p = new Patient();
            p.Id = 4;
            p.Name = "Dinesh1";
            p.MobileNo = "9840916032";
            p.Username = "Dinesh";
            p.Password = "Dinesh";
            pd.EditPatient(p);
            var er = pd.GetPaitentById(4).Name;
            var ar = "Dinesh1";
            Assert.AreEqual(er, ar);
        }
        [Test]
        public void PatientLogin()
        {
            Paitent_Dal pd = new Paitent_Dal();
            Patient p1 = pd.PatientLogin("Dinesh", "Dinesh");
            var er = pd.GetPaitentById(4).Name;
            var ar = p1.Name;
            Assert.AreEqual(er, ar);

        }



    }
}