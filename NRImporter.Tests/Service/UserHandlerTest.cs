using System;
using System.Collections.Generic;
using System.Configuration;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service;
using NUnit.Framework;

namespace NRImporter.Tests.Service
{
    [TestFixture]
    public class UserHandlerTest
    {

        private UserHandler _userHandler;

        [SetUp]
        public void Setup()
        {
            _userHandler =
                new UserHandler(
                    new UserService(new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString)));
            ;
            
        }

        [Test]
        public void TestLoadUserdetails()
        {
            var coll = new List<User>();
            var user = new User
            {
                Name = "Roland Gundersen",
                Email = "",
                Male = true
            };

            coll.Add(user);
            user = new User
            {
                Name = "Line Danser",
                Email = "",
                Male = false
            };
            coll.Add(user);
            var result = _userHandler.LoadUserDetails(coll);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
            foreach (var x in coll)
            {
                Console.WriteLine(x.Name + " " + x.ValidUser());
            }
        }
    }
}
