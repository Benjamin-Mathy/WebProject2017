using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebProject2017Server.EF.Mappers;
using WebProject2017Server.Modèle;
using System.Collections.Generic;
using WebProject2017Server.EF;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Validation;
using System.Linq.Expressions;

namespace WebProject2017.Tests.DB_Tests
{
    [TestClass]
    public class DB_Address_Test
    {
        private readonly string CONNECTION_STRING = "Test-WebProject2017-DB";

        Address AddressClassic1 = new Address() { Country = "Belgique", Locality = "Ouffet", Number = "7", PostalCode = "4590", Road = "Sauveniere" };
        Address AddressClassic2 = new Address() { Country = "Belgique", Locality = "Liege", Number = "82", PostalCode = "4000", Road = "Leman" };
        Address AddressClassic3 = new Address() { Country = "Belgique", Locality = "Liege", Number = "91", PostalCode = "4000", Road = "albert" };
        Address AddressWithoutCountry = new Address() { Locality = "Liège", Number = "80", PostalCode = "4000", Road = "Leman" };
        Address AddressWithoutLocality = new Address() { Country = "Belgique", Number = "80", PostalCode = "4000", Road = "Leman" };
        Address AddressWithoutNumber = new Address() { Country = "Belgique", Locality = "Liège", PostalCode = "4000", Road = "Leman" };
        Address AddressWithoutPostalCode = new Address() { Country = "Belgique", Locality = "Liège", Number = "80", Road = "Leman" };
        Address AddressWithoutRoad = new Address() { Country = "Belgique", Locality = "Liège", Number = "80", PostalCode = "4000" };
        Address AddressEmpty = new Address();


        [ClassInitialize]
        public static void SetupDabataseCreationStrategy(TestContext testContext)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DB_Context>());
        }

        [TestInitialize]
        public void Setup()
        {

        }
        //Add
        [TestMethod]
        public void AddAddressClassic()      {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(AddressClassic1);

                Assert.AreEqual(1, mapper.GetAll().Count);
            }
        }
        [TestMethod]
        public void AddAddresses()
        {
            List<Address> adresses = new List<Address> { AddressClassic1, AddressClassic2 };
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(adresses);

                Assert.AreEqual(3, mapper.GetAll().Count);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddAddressWithoutCountry()
        {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(AddressWithoutCountry);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddAddressWithoutLocality()
        {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(AddressWithoutLocality);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddAddressWithoutNumber()
        {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(AddressWithoutNumber);
                Assert.AreEqual(3, mapper.GetAll().Count);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddAddressWithoutPostalCode()
        {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(AddressWithoutPostalCode);
                Assert.AreEqual(3, mapper.GetAll().Count);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddAddressWithoutRoad()
        {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(AddressWithoutRoad);
                Assert.AreEqual(3, mapper.GetAll().Count);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddEmptyAddress()
        {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(AddressEmpty);
                Assert.AreEqual(3, mapper.GetAll().Count);
            }
        }
        //Get
        [TestMethod]
        public void GetAllAddress()
        {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                Assert.AreEqual(3, mapper.GetAll().Count);
            }
        }
        [TestMethod]
        public void GetOneAddressByID()
        {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                Assert.AreEqual(AddressClassic1.Road, mapper.FindBy(2).Road);
            }
        }
        [TestMethod]
        public void GetOneAddressBysearch()
        {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                Expression<Func<Address, bool>> expression = a => a.Road == AddressClassic2.Road && a.Number == AddressClassic2.Number;
                Assert.AreEqual(1, mapper.FindBy(expression).Count);
            }
        }
        //Edit
        [TestMethod]
        public void UpdateAddress()
        {           
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                AddressClassic3 = mapper.AddorUpdate(AddressClassic3);

                AddressClassic3.Number = "105";

                mapper.AddorUpdate(AddressClassic3);


                Expression<Func<Address, bool>> expression = a => a.ID == AddressClassic3.ID;
                Assert.AreEqual("105", mapper.FindBy(AddressClassic3.ID).Number);
            }
        }
        //Delete
        [TestMethod]
        public void DeleteAddress()
        {
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                mapper.Delete(3);

                Expression<Func<Address, bool>> expression = a => a.ID == 3;
                Assert.AreEqual(0, mapper.FindBy(expression).Count);
            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            
        }
    }
}
