using Microsoft.VisualStudio.TestTools.UnitTesting;
using Technotheek.net_Core.Containers;
using Technotheek.net_Core.ViewModels;
using Technotheek.Tests;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;

namespace TechnotheekUnitTests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Retreive_The_Correct_User_Data()
        {

            UserMock userMock = new UserMock();
            UserContainer userContainer = new UserContainer(userMock);

            var actual = userContainer.RetrieveUserData(1);
            var expected = "Yoda";

            Assert.AreEqual(expected, actual.Username);
        }

        [TestMethod]
        public void User_Logs_In_Correctely()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.Email = "Yoda";
            loginViewModel.Password = "123";

            UserMock userMock = new UserMock();
            User user = new User();
            UserContainer userContainer = new UserContainer(userMock);
            Login login = new Login();

            var actual = userContainer.LoginUser(loginViewModel);

            Assert.AreEqual("Yoda", actual.Username);
        }

        [TestMethod]
        public void User_Is_Registered_Correctely()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();

            registerViewModel.Username = "Yoda";

            UserMock userMock = new UserMock();
            UserContainer userContainer = new UserContainer(userMock);

            userContainer.AddUser(registerViewModel);
            var expected = "Yoda";

            Assert.AreEqual(expected, userMock.registerViewModel.Username);
        }
    }
}
