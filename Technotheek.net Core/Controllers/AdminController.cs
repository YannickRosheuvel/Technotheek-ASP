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
        BuildingContainer building = new BuildingContainer();
        static SongDAL songDAL = new SongDAL();
        SongContainer songContainer = new SongContainer(songDAL);
        List<RoomContainer> roomContainers = new List<RoomContainer>();

        private List<Rooms> rooms = new List<Rooms>
        {
            new Rooms(Rooms.SpaceBuilding.BigBuilding),
            new Rooms(Rooms.SpaceBuilding.SmallBuilding),
            new Rooms(Rooms.SpaceBuilding.MediumBuilding),
            new Rooms(Rooms.SpaceBuilding.MediumBuilding),
            new Rooms(Rooms.SpaceBuilding.BigBuilding),
            new Rooms(Rooms.SpaceBuilding.BigBuilding),
            new Rooms(Rooms.SpaceBuilding.MediumBuilding),
            new Rooms(Rooms.SpaceBuilding.SmallBuilding),
            new Rooms(Rooms.SpaceBuilding.BigBuilding),
            new Rooms(Rooms.SpaceBuilding.BigBuilding)

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

        static User currentAdmin;
        private readonly IWebHostEnvironment hostingEnviroment;

        public AdminController(IWebHostEnvironment hostingEnviroment)
        {
            this.hostingEnviroment = hostingEnviroment;
        }

        [HttpGet]
        public IActionResult Admin(User user)
        {
            if (HttpContext.Session.GetString("ID") != null)
            {
                currentAdmin = user;
                ViewBag.User = user;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
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

            building.AddEmployees(rooms, employees);
            
            foreach (RoomContainer currentRoom in building.ReturnRooms())
            {
                roomContainers.Add(currentRoom);
            }

            building.ReturnRooms();

            return Admin(currentAdmin);
        }

    }
}