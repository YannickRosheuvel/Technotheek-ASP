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
        Song song = new Song();
        static SongDAL songDAL = new SongDAL();
        SongContainer songContainer = new SongContainer(songDAL);

        static User currentAdmin;
        private readonly IWebHostEnvironment hostingEnviroment;

        public AdminController(IWebHostEnvironment hostingEnviroment)
        {
            this.hostingEnviroment = hostingEnviroment;
        }

        // GET: Admin
        [HttpGet]
        public ActionResult Admin(User user)
        {
            if (HttpContext.Session.GetString("ID") != null)
            {
                currentAdmin = user;
                ViewBag.User = user;
                return View();
            }
            else
            {
                return View("Account", "Login");
            }
        }

        [HttpPost]
        public IActionResult Upload(SongCreateViewModel model, string SongName)
        {
            //kijkt of er wel een nummer is geselecteerd om een null reference exception te voorkomen
            if (ModelState.IsValid)
            {
                if (model.Song != null)
                {
                    song.Name = SongName;
                    string UploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Music");
                    song.SongLink = Guid.NewGuid().ToString() + "_" + model.Song.FileName;
                    string filePath = Path.Combine(UploadsFolder, song.SongLink);
                    model.Song.CopyTo(new FileStream(filePath, FileMode.Create));
                    songContainer.NewSongAdd(song);
                }
            }
            return RedirectToAction("Admin");
        }

    }
}