using Microsoft.VisualStudio.TestTools.UnitTesting;
using Technotheek.net_Core.Containers;
using Technotheek.net_Core.LOGIC;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using Technotheek.Tests;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;

namespace TechnotheekUnitTests
{
    [TestClass]
    public class roomTests
    {
        [TestMethod]
        public void Retreive_The_Correct_User_Data()
        {
            RoomContainer room = new RoomContainer();
            Employee employee = new Employee()
            {
                employeeSpace = Employee.EmployeeSpace.Medium,
                Name = "",
                Added = false,
                functionType = Employee.FunctionType.SoftwareEngineer
            };

            room.CheckCompatibility();
        }
    }
}
