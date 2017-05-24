using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebProject2017Server.Modèle;
using WebProject2017Server.EF.Mappers;
using System.Data.Entity;
using WebProject2017Server.EF;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace WebProject2017.Tests.DB_Tests
{
    [TestClass]
    public class DB_Journey_Test
    {
        private readonly string CONNECTION_STRING = "Test-WebProject2017-DB";
        public List<Address> addresses;

        List<User> users;

        Journey journeyClassic1;
        Journey journeyClassic2;
        Journey journeyClassic3;
        Journey journeyClassic4;
        Journey journeyWithoutDescription;
        Journey journeyWithoutDriver;
        Journey journeyWithoutFreeSeat;
        Journey journeyWithoutStartingDateTime;
        Journey journeyWithoutDriverUpDateTime;
        Journey journeyWithoutStartingAddress;
        Journey journeyWithoutDriverUpAddress;
        Journey journeyEmpty;
        
        [ClassInitialize]
        public static void SetupDabataseCreationStrategy(TestContext testContext)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DB_Context>());                 
        }

        [TestInitialize]
        public void Setup()
        {
            addresses = new List<Address> {
                new Address() { Country = "Belgique", Locality = "Ouffet", Number = "7", PostalCode = "4590", Road = "Sauveniere" },
                new Address() { Country = "Belgique", Locality = "Liege", Number = "82", PostalCode = "4000", Road = "Leman" },
                new Address() { Country = "France", Locality = "Paris", Number = "100", PostalCode = "10000", Road = "efle" },
                new Address() { Country = "Italie", Locality = "Rome", Number = "97", PostalCode = "8520", Road = "pize" }
            };
            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
                addresses = mapper.AddorUpdate(addresses);
            }

            users = new List<User> {
                new User() { Login = "userJourney1", Password = "1234", Name = "bologne", LastName = "sauce", Email = "userJourney1@gmail.com", Phone = "0475859565", Rank = UserRank.Member, Address = addresses[0] }
                ,new User() { Login = "userJourney2", Password = "5896", Name = "coucou", LastName = "carle", Email = "userJourney2@skynet.be", Phone = "0465958565", Rank = UserRank.Member, Address = addresses[1] }
                ,new User() { Login = "userJourney3", Password = "8965", Name = "polo", LastName = "lolo", Email = "userJourney3@hotmail.fr", Phone = "0456958565", Rank = UserRank.Member, Address = addresses[2] }
            };
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                users =mapper.AddorUpdate(users);
            }
            
            journeyClassic1 = new Journey() {Description="trajet 1", Driver = users[0], FreeSeat=4, StartingAddress=addresses[0],DriverUpAddress=addresses[1],StartingDateTime=new DateTime(2017,06,12,14,30,0), DriverUpDatetime= new DateTime(2017, 06, 12, 18, 40, 0), Passengers = new List<User>() };
            journeyClassic2 = new Journey() {Description="trajet 2", Driver = users[1], FreeSeat=4, StartingAddress=addresses[2],DriverUpAddress=addresses[1],StartingDateTime=new DateTime(2017,06,12,14,30,0), DriverUpDatetime= new DateTime(2017, 06, 12, 18, 40, 0), Passengers = new List<User>() };
            journeyClassic3 = new Journey() {Description="trajet 3", Driver = users[0], FreeSeat=4, StartingAddress=addresses[3],DriverUpAddress=addresses[2],StartingDateTime=new DateTime(2017,06,12,14,30,0), DriverUpDatetime= new DateTime(2017, 06, 12, 18, 40, 0), Passengers = new List<User>() };
            journeyClassic4 = new Journey() {Description="trajet 4", Driver = users[0], FreeSeat=4, StartingAddress=addresses[3],DriverUpAddress=addresses[2],StartingDateTime=new DateTime(2017,06,12,14,30,0), DriverUpDatetime= new DateTime(2017, 06, 12, 18, 40, 0), Passengers = new List<User>() };
            journeyWithoutDescription = new Journey() {Driver= users[0], FreeSeat=4, StartingAddress= addresses[0], DriverUpAddress= addresses[1], StartingDateTime=new DateTime(2017,06,12,14,30,0), DriverUpDatetime= new DateTime(2017, 06, 12, 18, 40, 0), Passengers = new List<User>() };
            journeyWithoutFreeSeat = new Journey() {Description="trajet 1", Driver= users[0], StartingAddress= addresses[0], DriverUpAddress= addresses[1], StartingDateTime=new DateTime(2017,06,12,14,30,0), DriverUpDatetime= new DateTime(2017, 06, 12, 18, 40, 0), Passengers = new List<User>() };
            journeyWithoutDriver = new Journey() {Description="trajet 1", FreeSeat=4, StartingAddress= addresses[0], DriverUpAddress= addresses[1], StartingDateTime=new DateTime(2017,06,12,14,30,0), DriverUpDatetime= new DateTime(2017, 06, 12, 18, 40, 0), Passengers = new List<User>() };
            journeyWithoutStartingAddress = new Journey() {Description="trajet 1", Driver= users[0], FreeSeat=4,DriverUpAddress= addresses[1], StartingDateTime=new DateTime(2017,06,12,14,30,0), DriverUpDatetime= new DateTime(2017, 06, 12, 18, 40, 0), Passengers = new List<User>() };
            journeyWithoutDriverUpAddress = new Journey() {Description="trajet 1", Driver= users[0], FreeSeat=4, StartingAddress= addresses[0], StartingDateTime=new DateTime(2017,06,12,14,30,0), DriverUpDatetime= new DateTime(2017, 06, 12, 18, 40, 0), Passengers = new List<User>() };
            journeyWithoutStartingDateTime = new Journey() {Description="trajet 1", Driver= users[0], FreeSeat=4, StartingAddress= addresses[0], DriverUpAddress= addresses[1], DriverUpDatetime= new DateTime(2017, 06, 12, 18, 40, 0), Passengers = new List<User>() };
            journeyWithoutDriverUpDateTime = new Journey() {Description="trajet 1", Driver= users[0], FreeSeat=4, StartingAddress= addresses[0], DriverUpAddress= addresses[1], StartingDateTime=new DateTime(2017,06,12,14,30,0), Passengers = new List<User>() };
            journeyEmpty = new Journey();
        }
        //Add
        [TestMethod]
        public void AddJourneyClassic()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                journeyClassic1 = mapper.AddorUpdate(journeyClassic1);
                Assert.AreNotEqual(0, journeyClassic1.ID);
            }
        }
        [TestMethod]
        public void AddJounreys()
        {
            List<Journey> journeys = new List<Journey>
            {
                journeyClassic2,journeyClassic3
            };
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                Assert.AreNotEqual(0, mapper.AddorUpdate(journeys).First().ID);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddJourneyWithoutDescription()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(journeyWithoutDescription);
            }
        }
        [TestMethod]
        public void AddJourneyWithoutFreeSeat()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                Assert.AreNotEqual(0, mapper.AddorUpdate(journeyWithoutFreeSeat));
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddJourneyWithoutDriver()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(journeyWithoutDriver);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddJourneyWithoutSartingAddress()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(journeyWithoutStartingAddress);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddJourneyWithoutDriverUpAddress()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(journeyWithoutDriverUpAddress);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddJourneyWithoutSartingDateTime()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(journeyWithoutStartingDateTime);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddJourneyWithoutDriverUpDateTime()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(journeyWithoutDriverUpDateTime);
            }
        }        
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddEmptyJourney()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(journeyEmpty);
            }
        }
        //Get
        [TestMethod]
        public void GetAllJounrey()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                Assert.AreNotEqual(0, mapper.GetAll().Count);
            }
        }
        [TestMethod]
        public void GetOneJourneyByID()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                Assert.AreEqual(journeyClassic1.Description, mapper.FindBy(1).Description);
            }
        }
        [TestMethod]
        public void GetOneJourneyBysearch()
        {
            Journey journey = journeyClassic4;
            journey.ID = 0;
            journey.FreeSeat = 10;
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                journey = mapper.AddorUpdate(journey);

                Expression<Func<Journey, bool>> expression = j => j.Description == journey.Description && j.Driver == journey.Driver;
                Assert.AreEqual(journey.ID, mapper.FindBy(expression).First().ID);
            }
        }
        //Edit
        [TestMethod]
        public void UpdateJourney()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                journeyClassic4 = mapper.AddorUpdate(journeyClassic4);

                journeyClassic4.Description = "coucou";
                journeyClassic4= mapper.AddorUpdate(journeyClassic4);

                Assert.AreEqual("coucou", mapper.FindBy(journeyClassic4.ID).Description);
            }
        }
        [TestMethod]
        public void UpdateJourneyAddPassenger()
        {
            journeyClassic4.ID = 0;
            journeyClassic4.Description = "trajet 5";
            journeyClassic4.Driver = users[1];
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                journeyClassic4 = mapper.AddorUpdate(journeyClassic4);                
                   
                journeyClassic4.Passengers.Add(users[0]);
                mapper.AddorUpdate(journeyClassic4);

                Assert.AreEqual(users[0].Login, mapper.FindBy(journeyClassic4.ID).Passengers.First().Login);
            }
        }
        //Delete
        [TestMethod]
        public void DeleteJourney()
        {
            using (EFJourneyMapper mapper = new EFJourneyMapper(CONNECTION_STRING))
            {
                mapper.Delete(2);

                Expression<Func<Journey, bool>> expression = j => j.ID == 2;
                Assert.AreEqual(0, mapper.FindBy(expression).Count);
            }
        }
        //Cleanup
        [TestCleanup]
        public void Cleanup()
        {

        }

    }
}
