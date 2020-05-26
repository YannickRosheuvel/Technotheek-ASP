﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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

        private readonly IWebHostEnvironment hostingEnviroment;

        public AdminController(IWebHostEnvironment hostingEnviroment)
        {
            this.hostingEnviroment = hostingEnviroment;
        }

        // GET: Admin
        [HttpGet]
        public ActionResult Admin()
        {

            return View("Admin");
        }

        [HttpPost]
        public IActionResult Upload(SongCreateViewModel model, string SongName)
        {
            //kijkt of er wel een nummer is geselecteerd om een null reference exception te voorkomen
            if (ModelState.IsValid)
            {
                if (model.Song != null)
                {
                    song.NameSong = SongName;
                    string UploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "Music");
                    song.SongPath = Guid.NewGuid().ToString() + "_" + model.Song.FileName;
                    string filePath = Path.Combine(UploadsFolder, song.SongPath);
                    model.Song.CopyTo(new FileStream(filePath, FileMode.Create));
                    songContainer.NewSongAdd(song);
                }
            }
            return RedirectToAction("Admin");
        }

    }
}