using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northernrunners.ImportLibrary.Service;
using NUnit.Framework;

namespace NRImporter.Tests.Service
{

    [TestFixture]
    public class UserServiceTest
    {
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new UserService(new SqlDirectService(ConfigurationManager.ConnectionStrings["db"].ConnectionString));
        }

        [Test]
        public void TestGetAllUsers()
        {
            var users = _userService.GetAllUsers();
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
            Console.WriteLine(users.Count);
        }

        [TestCase("demo")]
        public void TestGetUser(string name)
        {
            Assert.IsNotNull(_userService.FindUser(name));
        }
    }
}
