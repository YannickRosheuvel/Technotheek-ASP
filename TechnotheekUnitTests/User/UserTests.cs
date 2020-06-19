using Microsoft.VisualStudio.TestTools.UnitTesting;
using Technotheek.net_Core.Containers;
using Technotheek.net_Core.ViewModels;
using Technotheek.Tests;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;

namespace TechnotheekUnitTests
{
    [TestClass]
    public class RoomTest
    {
        [TestMethod]
        public void Retreive_The_Correct_User_Data()
        {

            UserMock userMock = new UserMock();
            UserContainer userContainer = new UserContainer(userMock);

            var actual = userContainer.RetrieveUserData(1);
            var expected = "yannick.rosheuvel@gmail.com";

            Assert.AreEqual(expected, actual.Username);
        }

        [TestMethod]
        public void User_Logs_In_Correctely()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.Email = "yannick.rosheuvel@gmail.com";
            loginViewModel.Password = "123";

            UserMock userMock = new UserMock();
            User user = new User();
            UserContainer userContainer = new UserContainer(userMock);
            Login login = new Login();

            var actual = userContainer.LoginUser(loginViewModel);

            Assert.AreEqual("yannick.rosheuvel@gmail.com", actual.Username);
        }

        [TestMethod]
        public void User_Is_Registered_Correctely()
        {
            UserMock userMock = new UserMock();
            UserContainer userContainer = new UserContainer(userMock);

            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.Username = "yannick.rosheuvel@gmail.com";

            var actual = userContainer.AddUser(registerViewModel);
            var expected = "yannick.rosheuvel@gmail.com";

            Assert.AreEqual(expected, actual.Username);
        }

        [TestMethod]
        public void Gets_Correct_Picture_Location()
        {
            UserMock userMock = new UserMock();
            UserContainer userContainer = new UserContainer(userMock);

            var actual = userContainer.GetPictureUser(1);
            var expected = "pictureLocation";

            Assert.AreEqual(expected, actual);
        }
    }
}
