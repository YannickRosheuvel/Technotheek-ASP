using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Technotheek.net_Core.LOGIC;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;
using Technotheek.net_Core.Models.RoomSpace;

namespace TechnotheekWeb.Controllers
{
    public class AdminController : Controller
    {
        BuildingContainer buildingContainer;
        static SongDAL songDAL;
        SongContainer songContainer;
        List<RoomContainer> roomContainers;
        List<Employee> employeesInRoom;
        private readonly IWebHostEnvironment hostingEnviroment;

        static User currentAdmin;

        public AdminController(IWebHostEnvironment hostingEnviroment)
        {
            employeesInRoom = new List<Employee>();
            buildingContainer = new BuildingContainer();
            songDAL = new SongDAL();
            songContainer = new SongContainer(songDAL);
            roomContainers = new List<RoomContainer>();
            this.hostingEnviroment = hostingEnviroment;
        }

        private List<Room> rooms = new List<Room>
        {
            new Room(Room.SpaceBuilding.BigBuilding),
            new Room(Room.SpaceBuilding.SmallBuilding),
            new Room(Room.SpaceBuilding.MediumBuilding),
            new Room(Room.SpaceBuilding.MediumBuilding),
            new Room(Room.SpaceBuilding.BigBuilding),
            new Room(Room.SpaceBuilding.BigBuilding),
            new Room(Room.SpaceBuilding.MediumBuilding),
            new Room(Room.SpaceBuilding.SmallBuilding),
            new Room(Room.SpaceBuilding.BigBuilding),
            new Room(Room.SpaceBuilding.BigBuilding)

        };

        private List<Employee> employees = new List<Employee>
        {
        new Employee(Employee.EmployeeSpace.Big, "Femke", Employee.FunctionType.HardwareEngineer),
        new Employee(Employee.EmployeeSpace.Medium, "Floris", Employee.FunctionType.ProjectManager),
        new Employee(Employee.EmployeeSpace.Medium, "Quincy", Employee.FunctionType.HardwareEngineer),
        new Employee(Employee.EmployeeSpace.Big, "Indigo", Employee.FunctionType.ProjectManager),
        new Employee(Employee.EmployeeSpace.Medium, "Jumeira", Employee.FunctionType.SoftwareEngineer),
        new Employee(Employee.EmployeeSpace.Big, "Theo", Employee.FunctionType.SoftwareEngineer),
        new Employee(Employee.EmployeeSpace.Medium, "Bas", Employee.FunctionType.ProjectManager),
        new Employee(Employee.EmployeeSpace.Small, "Mohammed", Employee.FunctionType.SoftwareEngineer),
        new Employee(Employee.EmployeeSpace.Medium, "Erik", Employee.FunctionType.SoftwareEngineer),
        new Employee(Employee.EmployeeSpace.Medium, "Youssef", Employee.FunctionType.HardwareEngineer),
        new Employee(Employee.EmployeeSpace.Small, "Jens", Employee.FunctionType.SoftwareEngineer),
        new Employee(Employee.EmployeeSpace.Small, "Yannick", Employee.FunctionType.SoftwareEngineer),
        new Employee(Employee.EmployeeSpace.Small, "Femke", Employee.FunctionType.ProjectManager),
        new Employee(Employee.EmployeeSpace.Big, "Thomas", Employee.FunctionType.SoftwareEngineer),
        new Employee(Employee.EmployeeSpace.Small, "Julius", Employee.FunctionType.ProjectManager),
        new Employee(Employee.EmployeeSpace.Big, "Merijn", Employee.FunctionType.ProjectManager)


        };

        public IActionResult Index(User user)
        {
            if (HttpContext.Session.GetString("ID") != null)
            {
                currentAdmin = user;
                ViewBag.User = user;

                return RedirectToAction("Index", "Home", user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Admin(List<RoomContainer> rooms)
        {
            if (HttpContext.Session.GetString("ID") != null)
            {
                ViewBag.Rooms = rooms;
                ViewBag.User = currentAdmin;

                return View("Admin");
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        public IActionResult SongAdmin()
        {
            ViewBag.User = currentAdmin;
            return View("Admin");
        }

        public IActionResult ViewEmployees(List<Employee> employees)
        {
            ViewBag.User = currentAdmin;

            return View("Employees", employees);
        }

        [HttpPost]
        public IActionResult Upload(SongCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Song != null)
                {
                    string UploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Music");
                    model.SongLink = Guid.NewGuid().ToString() + "_" + model.Song.FileName;

                    string filePath = Path.Combine(UploadsFolder, model.SongLink);
                    model.Song.CopyTo(new FileStream(filePath, FileMode.Create));

                    songContainer.NewSongAdd(model);
                }
            }
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult FillRooms(IEnumerable<EmployeeDetailsViewModel> listOfEmployeeDataViewModels)
        {

            buildingContainer.AddEmployees(rooms, employees);
            
            foreach (var currentRoom in buildingContainer.ReturnRooms())
            {
                roomContainers.Add(currentRoom);
            }

            ViewBag.User = currentAdmin;
            ViewBag.Rooms = roomContainers;
            return View("Admin");
        }

    }
}