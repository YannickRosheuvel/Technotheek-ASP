using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Technotheek.net_Core.Containers;
using Technotheek.net_Core.LOGIC;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using Technotheek.Tests;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;
using static Technotheek.net_Core.Models.Room;

namespace TechnotheekUnitTests
{
    [TestClass]
    public class BuildingContainerTests
    {

        [TestMethod]
        public void Employees_Are_Sorted_Correctly()
        {
            BuildingContainer buildingContainer = new BuildingContainer();

            List<Employee> employees = new List<Employee>()
            {
                new Employee(Employee.EmployeeSpace.Small, "Thomas", Employee.FunctionType.SoftwareEngineer),
                new Employee(Employee.EmployeeSpace.Small, "Femke", Employee.FunctionType.ProjectManager),
                new Employee(Employee.EmployeeSpace.Big, "Julius", Employee.FunctionType.SoftwareEngineer),
                new Employee(Employee.EmployeeSpace.Big, "Merijn", Employee.FunctionType.ProjectManager)
            };

            var actual = buildingContainer.SortEmployees(employees);

            Assert.AreEqual(Employee.FunctionType.ProjectManager, actual[0].functionType);
            Assert.AreEqual(Employee.EmployeeSpace.Small, actual[3].employeeSpace);
        }

        [TestMethod]
        public void Rooms_Are_Sorted_Correctly()
        {
            BuildingContainer buildingContainer = new BuildingContainer();

            List<Room> rooms = new List<Room>()
            {
            new Room(SpaceBuilding.SmallBuilding),
            new Room(SpaceBuilding.SmallBuilding),
            new Room(SpaceBuilding.MediumBuilding),
            new Room(SpaceBuilding.SmallBuilding),
            new Room(SpaceBuilding.BigBuilding),
            new Room(SpaceBuilding.MediumBuilding)
            };

            var actual = buildingContainer.SortRooms(rooms);

            Assert.AreEqual(SpaceBuilding.BigBuilding, actual[0].buildingSpace);
            Assert.AreEqual(SpaceBuilding.SmallBuilding, actual[5].buildingSpace);
        }

        [TestMethod]
        public void Only_Increment_To_The_Max_Amount_Of_Rooms()
        {
            BuildingContainer buildingContainer = new BuildingContainer();

            List<Room> rooms = new List<Room>()
            {
            new Room(SpaceBuilding.SmallBuilding),
            new Room(SpaceBuilding.SmallBuilding),
            };

            var actualIsTrue = buildingContainer.NextRoom(rooms);
            var actualIsFalse = buildingContainer.NextRoom(rooms);

            Assert.IsTrue(actualIsTrue);
            Assert.IsFalse(actualIsFalse);
        }

        [TestMethod]
        public void Return_Employees_Where_Added_Is_False()
        {
            BuildingContainer buildingContainer = new BuildingContainer();

            List<Employee> employees = new List<Employee>()
            {
                new Employee(Employee.EmployeeSpace.Small, "Thomas", Employee.FunctionType.SoftwareEngineer),
                new Employee(Employee.EmployeeSpace.Small, "Femke", Employee.FunctionType.ProjectManager),
            };

            var actual = buildingContainer.GetUnAddedEmployees(employees).Count;
            var expected = 2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Check_If_Two_Small_Employees_Fit_In_Small_Room()
        {
            BuildingContainer buildingContainer = new BuildingContainer();
            List<Employee> filledEmployees = new List<Employee>();

            List<Room> rooms = new List<Room>()
            {
            new Room(SpaceBuilding.SmallBuilding),
            };

            List<Employee> employees = new List<Employee>()
            {
                new Employee(Employee.EmployeeSpace.Small, "Thomas", Employee.FunctionType.SoftwareEngineer),
                new Employee(Employee.EmployeeSpace.Small, "Thomas", Employee.FunctionType.SoftwareEngineer)
            };

            buildingContainer.AddEmployees(rooms, employees);

            RoomContainer selectedRoom = buildingContainer.ReturnRooms()[0];

            foreach (Employee employee in selectedRoom.ReturnEmployees())
            {
                filledEmployees.Add(employee);
            }

            var actual = filledEmployees;
            var expected = 2;

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void Second_Employee_Cannot_Fit_In_Room()
        {
            BuildingContainer buildingContainer = new BuildingContainer();
            List<Employee> filledEmployees = new List<Employee>();

            List<Room> rooms = new List<Room>()
            {
            new Room(SpaceBuilding.SmallBuilding),
            };

            List<Employee> employees = new List<Employee>()
            {
                new Employee(Employee.EmployeeSpace.Small, "Thomas", Employee.FunctionType.SoftwareEngineer),
                new Employee(Employee.EmployeeSpace.Big, "Thomas", Employee.FunctionType.SoftwareEngineer)
            };

            buildingContainer.AddEmployees(rooms, employees);

            RoomContainer selectedRoom = buildingContainer.ReturnRooms()[0];

            foreach (Employee employee in selectedRoom.ReturnEmployees())
            {
                filledEmployees.Add(employee);
            }

            var actual = filledEmployees;
            var expected = 1;

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void Two_Project_Managers_Cant_Be_In_Same_Room()
        {
            BuildingContainer buildingContainer = new BuildingContainer();
            List<Employee> filledEmployees = new List<Employee>();

            List<Room> rooms = new List<Room>()
            {
            new Room(SpaceBuilding.SmallBuilding),
            };

            List<Employee> employees = new List<Employee>()
            {
                new Employee(Employee.EmployeeSpace.Small, "Thomas", Employee.FunctionType.ProjectManager),
                new Employee(Employee.EmployeeSpace.Big, "Thomas", Employee.FunctionType.ProjectManager)
            };

            buildingContainer.AddEmployees(rooms, employees);

            RoomContainer selectedRoom = buildingContainer.ReturnRooms()[0];

            foreach (Employee employee in selectedRoom.ReturnEmployees())
            {
                filledEmployees.Add(employee);
            }

            var actual = filledEmployees;
            var expected = 1;

            Assert.AreEqual(expected, actual.Count);
        }

        [TestMethod]
        public void Initial_Unadded_Employees_Are_Added_Afterwards()
        {
            BuildingContainer buildingContainer = new BuildingContainer();
            List<Employee> filledEmployees = new List<Employee>();

            List<Room> rooms = new List<Room>()
            {
            new Room(SpaceBuilding.SmallBuilding),
            new Room(SpaceBuilding.SmallBuilding)
            };

            List<Employee> employees = new List<Employee>()
            {
                new Employee(Employee.EmployeeSpace.Small, "Thomas", Employee.FunctionType.ProjectManager),
                new Employee(Employee.EmployeeSpace.Small, "Thomas", Employee.FunctionType.ProjectManager),
                new Employee(Employee.EmployeeSpace.Small, "Thomas", Employee.FunctionType.SoftwareEngineer),
                new Employee(Employee.EmployeeSpace.Small, "Thomas", Employee.FunctionType.SoftwareEngineer)
            };

            buildingContainer.AddEmployees(rooms, employees);

            RoomContainer selectedRoom1 = buildingContainer.ReturnRooms()[0];

            foreach (Employee employee in selectedRoom1.ReturnEmployees())
            {
                filledEmployees.Add(employee);
            }

            RoomContainer selectedRoom2 = buildingContainer.ReturnRooms()[1];

            foreach (Employee employee in selectedRoom2.ReturnEmployees())
            {
                filledEmployees.Add(employee);
            }

            var actual = filledEmployees;
            var expected = 4;

            Assert.AreEqual(expected, actual.Count);
        }
    }
}
