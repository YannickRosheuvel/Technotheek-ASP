using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Technotheek.net_Core.Containers;
using Technotheek.net_Core.LOGIC;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.Models.RoomSpace;
using Technotheek.net_Core.ViewModels;
using Technotheek.Tests;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;
using static Technotheek.net_Core.Models.RoomSpace.Room;

namespace TechnotheekUnitTests
{
    [TestClass]
    public class RoomContainertests
    {

        [TestMethod]
        public void Project_Leaders_Cant_Be_In_Same_Room()
        {
            RoomContainer roomContainer = new RoomContainer();

            Employee employee = new Employee(Employee.EmployeeSpace.Small, "Femke", Employee.FunctionType.ProjectManager);

            List<Employee> employeesInRoom = new List<Employee>()
            {
                 new Employee(Employee.EmployeeSpace.Big, "Merijn", Employee.FunctionType.ProjectManager)
            };

            var actual = roomContainer.CheckCompatibility(employee, employeesInRoom);

            Assert.IsFalse(actual);

        }

        [TestMethod]
        public void Software_And_Hardware_Engineer_Cant_Be_In_Same_Room()
        {
            RoomContainer roomContainer = new RoomContainer();

            Employee employee = new Employee(Employee.EmployeeSpace.Small, "Femke", Employee.FunctionType.HardwareEngineer);

            List<Employee> employeesInRoom = new List<Employee>()
            {
                 new Employee(Employee.EmployeeSpace.Big, "Merijn", Employee.FunctionType.SoftwareEngineer)
            };

            var actual = roomContainer.CheckCompatibility(employee, employeesInRoom);

            Assert.IsFalse(actual);

        }

        [TestMethod]
        public void Two_Of_Same_Type_Engineers_Can_Be_In_Same_Room()
        {
            RoomContainer roomContainer = new RoomContainer();

            Employee employee = new Employee(Employee.EmployeeSpace.Small, "Femke", Employee.FunctionType.SoftwareEngineer);

            List<Employee> employeesInRoom = new List<Employee>()
            {
                 new Employee(Employee.EmployeeSpace.Big, "Merijn", Employee.FunctionType.SoftwareEngineer)
            };

            var actual = roomContainer.CheckCompatibility(employee, employeesInRoom);

            Assert.IsTrue(actual);

        }

        [TestMethod]
        public void asfdsaf()
        {
            RoomContainer roomContainer = new RoomContainer();

            Employee Femke = new Employee(Employee.EmployeeSpace.Big, "Femke", Employee.FunctionType.SoftwareEngineer);
            Employee Kim = new Employee(Employee.EmployeeSpace.Medium, "Kim", Employee.FunctionType.SoftwareEngineer);

            var actual1 = roomContainer.CheckRoom(Femke, 4);
            var actual2 = roomContainer.CheckRoom(Kim, 4);

            Assert.IsFalse(actual1);
            Assert.IsTrue(actual2);
        }
    }
}
