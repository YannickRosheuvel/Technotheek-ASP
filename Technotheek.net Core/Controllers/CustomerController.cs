using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NAudio.Wave;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using TechnotheekWeb.Containers;
using TechnotheekWeb.DAL;
using TechnotheekWeb.Models;
using TechnotheekWeb.ViewModels;

namespace TechnotheekWeb.Controllers
{
    public class CustomerController : Controller
    {
        static SongDAL songDAL = new SongDAL();
        static UserDAL userDAL = new UserDAL();
        SongContainer songContainer = new SongContainer(songDAL);
        UserContainer userContainer = new UserContainer(userDAL);
        SongViewModel songViewModel = new SongViewModel();
        Song song = new Song();
        User user = new User();
        // GET: Costumer

        static User currentUser;

        private readonly IWebHostEnvironment hostingEnviroment;

        public CustomerController(IWebHostEnvironment hostingEnviroment)
        {
            this.hostingEnviroment = hostingEnviroment;
        }

        [HttpGet]
        public ViewResult Customer()
        {
            var model = songContainer.ReturnAllSongs();
            if (HttpContext.Session.GetString("ID") != null)
            {
                ViewBag.User = currentUser;
                return View("Customer", model);
                //return View();
            }
            else
            {
                return View("Account", "Login");
            }
        }

        [HttpPost]
        public ViewResult Search(string NameSong)
        {
            song.Name = NameSong;
            var model = songContainer.SearchSong(song);
            songViewModel.SongsFound =  "Songs found: " + model.Count.ToString();
            return View("Customer", model);
        }

        public ViewResult Play(string selectedSong)
        {
            ViewBag.SelectedSong = songContainer.GetSelectedSongPath(selectedSong);
            return Customer();
        }

        public ViewResult Playlist()
        {
            if (HttpContext.Session.GetString("ID") != null)
            {
                ViewBag.Playlists = songContainer.GetPlaylists(currentUser.ID);
                ViewBag.User = currentUser;
                return View();
            }
            else
            {
                return View("Account", "Login");
            }
        }

        public IActionResult CreatePlaylist(string PlaylistName)
        {
            songContainer.MakeNewPlaylist(PlaylistName, currentUser.ID);
            return RedirectToAction("Playlist", "Customer");
        }

        public ViewResult AddSongToPlaylist(string songToAdd)
        {

            return Customer();
        }

        public IActionResult Index(User user)
        {
            if (HttpContext.Session.GetString("ID") != null)
            {
                currentUser = user;
                ViewBag.User = user;
                return RedirectToAction("Index", "Home", user);
                //return View();
            }
            else
            {
                return RedirectToAction("Account", "Login");
            }
        }

        [HttpPost]
        public IActionResult Upload(ProfilePictureViewModel model)
        {
            User user = new User();
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "ProfilePictures");
                    user.PictureLocation = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, user.PictureLocation);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    userContainer.ReturnInsertImage(user, currentUser.ID);
                }
            }
            return RedirectToAction("userPage" , "Home");
        }
    }
}