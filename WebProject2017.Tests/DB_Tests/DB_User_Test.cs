using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebProject2017Server.EF.Mappers;
using System.Data.Entity;
using WebProject2017Server.EF;
using WebProject2017Server.Modèle;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Collections.Generic;

namespace WebProject2017.Tests.DB_Tests
{
    [TestClass]
    public class DB_User_Test
    {
        private readonly string CONNECTION_STRING = "Test-WebProject2017-DB";

        List<Address> addresses;

        Address AddressClassic3;

        User userClassic1;
        User userClassic2;
        User userClassic3;
        User userClassic4;
        User userClassic5;
        User userWithoutLogin;
        User userWithoutPassword;
        User userWithoutLastName;
        User userWithoutName;
        User userWithoutEmail;
        User userWithoutPhone;
        User userWithoutUserRank;
        User userWithoutAddress;
        User userEmpty;

        [ClassInitialize]
        public static void SetupDabataseCreationStrategy(TestContext testContext)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DB_Context>());
        }

        [TestInitialize]
        public void Setup()
        {

            EFAddressMapper addressMapper = new EFAddressMapper("Test-WebProject2017-DB");
            addresses = new List<Address> {
                new Address() { Country = "Belgique", Locality = "Ouffet", Number = "7", PostalCode = "4590", Road = "Sauveniere" },
                new Address() { Country = "Belgique", Locality = "Liege", Number = "82", PostalCode = "4000", Road = "Leman" }
            };
            AddressClassic3 = new Address() { Country = "France", Locality = "Paris", Number = "100", PostalCode = "10000", Road = "efle" };

            using (EFAddressMapper mapper = new EFAddressMapper(CONNECTION_STRING))
            {
               addresses= mapper.AddorUpdate(addresses);
            }

            userClassic1 = new User() { Login = "log", Password = "1234", Name = "bologne", LastName = "sauce", Email = "email@gmail.com", Phone = "0475859565", Rank = UserRank.Member, Address = addresses.First()  };
            userClassic2 = new User() { Login = "gol2", Password = "5896", Name = "coucou", LastName="carle", Email="carleCoucou@skynet.be", Phone="0465958565", Rank = UserRank.Member, Address = addresses.Last() };
            userClassic3 = new User() { Login = "polo", Password = "8965", Name = "polo", LastName="lolo", Email="polololo@hotmail.fr", Phone="0456958565", Rank = UserRank.Member, Address = addresses.Last()  };
            userClassic4 = new User() { Login = "dolioe", Password = "8965", Name = "polo", LastName="lolo", Email="pololilu@hotmail.fr", Phone="0456958565", Rank = UserRank.Member, Address = addresses.Last()  };
            userClassic5 = new User() { Login = "doltre", Password = "8965", Name = "polo", LastName="lolo", Email="pololela@hotmail.fr", Phone="0456958565", Rank = UserRank.Member, Address = addresses.Last()  };
            userWithoutLogin = new User() { Password = "1234", Name = "bologne", LastName="sauce", Email="email2@gmail.com", Phone="0475859565", Rank = UserRank.Member, Address = addresses.First()  };
            userWithoutPassword = new User() { Login = "log2", Name = "bologne", LastName="sauce", Email="email3@gmail.com", Phone="0475859565", Rank = UserRank.Member, Address = addresses.First() };
            userWithoutName = new User() { Login = "log3", Password = "1234", LastName="sauce", Email="email4@gmail.com", Phone="0475859565", Rank = UserRank.Member, Address = addresses.First() };
            userWithoutLastName = new User() { Login = "log4", Password = "1234", Name = "bologne", Email="email5@gmail.com", Phone="0475859565", Rank = UserRank.Member, Address = addresses.First() };
            userWithoutEmail = new User() { Login = "log5", Password = "1234", Name = "bologne", LastName="sauce", Phone="0475859565", Rank = UserRank.Member, Address = addresses.First() };
            userWithoutPhone = new User() { Login = "log6", Password = "1234", Name = "bologne", LastName="sauce", Email="email6@gmail.com", Rank = UserRank.Member, Address = addresses.First() };
            userWithoutAddress = new User() { Login = "loga", Password = "1234", Name = "bologne", LastName="sauce", Email="emaila@gmail.com", Phone="0475859565", Rank = UserRank.Member  };
            userWithoutUserRank = new User() { Login = "logr", Password = "1234", Name = "bologne", LastName="sauce", Email="emailr@gmail.com", Phone="0475859565", Address = addresses.First() };
            userEmpty = new User();
            
        }
        //Add
        [TestMethod]
        public void AddUserClassic()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                Assert.AreNotEqual(0, mapper.AddorUpdate(userClassic3).ID);
            }
        }
        [TestMethod]
        public void AddUsers()
        {
            List<User> users = new List<User> { userClassic1, userClassic2 };
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                Assert.AreNotEqual(0, mapper.AddorUpdate(users).Count);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddUserWithoutLogin()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(userWithoutLogin);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddUserWithoutPassword()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(userWithoutPassword);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddUserWithoutName()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(userWithoutName);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddUserWithoutLastName()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(userWithoutLastName);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddUserWithoutEmail()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(userWithoutEmail);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddUserWithoutPhone()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(userWithoutPhone);
            }
        }
        [TestMethod]
        public void AddUserWithoutAddress()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                Assert.AreEqual(userWithoutAddress.Login, mapper.AddorUpdate(userWithoutAddress).Login);
            }
        }
        [TestMethod]
        public void AddUserWithoutUserRank()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                Assert.AreEqual(userWithoutUserRank.Login, mapper.AddorUpdate(userWithoutUserRank).Login);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddEmptyUser()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                mapper.AddorUpdate(userEmpty);
            }
        }
        //Get
        [TestMethod]
        public void GetAllUser()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                Assert.AreNotEqual(0, mapper.GetAll().Count);
            }
        }
        [TestMethod]
        public void GetOneUserByID()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                userClassic4 = mapper.AddorUpdate(userClassic4);
                Assert.AreEqual(userClassic4.Login, mapper.FindBy(userClassic4.ID).Login);
            }
        }
        [TestMethod]
        public void GetOneUserBysearch()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                Expression<Func<User, bool>> expression = u => u.Email == userClassic2.Email && u.Phone == userClassic2.Phone;
                Assert.AreEqual(userClassic2.Login, mapper.FindBy(expression).First().Login);
            }
        }
        //Edit
        [TestMethod]
        public void UpdateUser()
        {
            userClassic1.Phone = "0478956321";
            userClassic1.ID = 2;
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                userClassic5 = mapper.AddorUpdate(userClassic5);
                userClassic5.Phone = "0478956321";

                userClassic5 = mapper.AddorUpdate(userClassic5);

                Assert.AreEqual("0478956321", mapper.FindBy(userClassic5.ID).Phone);
            }
        }
        [TestMethod]
        public void UpdateUserAddAddress()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {

                Expression<Func<User, bool>> expression = u => u.Login == userWithoutAddress.Login;
                User us = mapper.FindBy(expression).First();
                us.Address = AddressClassic3;
                mapper.AddorUpdate(us);

                Assert.AreEqual(AddressClassic3, mapper.FindBy(us.ID).Address);
            }
        }
        //Delete
        [TestMethod]
        public void DeleteUser()
        {
            using (EFUserMapper mapper = new EFUserMapper(CONNECTION_STRING))
            {
                Expression<Func<User, bool>> expression = u => u.Login == userClassic3.Login;

                mapper.Delete(mapper.FindBy(expression).First().ID);

                Assert.AreEqual(0, mapper.FindBy(expression).Count);
            }
        }
        [TestCleanup]
        public void Cleanup()
        {

        }
    }    
}
