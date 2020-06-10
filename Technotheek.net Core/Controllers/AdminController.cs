using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;

namespace TechnotheekWeb.Controllers
{
    public class AdminController : Controller
    {
        static SongDAL songDAL = new SongDAL();
        SongContainer songContainer = new SongContainer(songDAL);

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

    }
}