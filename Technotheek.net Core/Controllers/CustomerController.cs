using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NAudio.Wave;
using Technotheek.net_Core.Models;
using TechnotheekWeb.Containers;
using TechnotheekWeb.Models;
using TechnotheekWeb.ViewModels;

namespace TechnotheekWeb.Controllers
{
    public class CustomerController : Controller
    {
        static SongDAL songDAL = new SongDAL();
        SongContainer songContainer = new SongContainer(songDAL);
        SongViewModel songViewModel = new SongViewModel();
        Song song = new Song();
        // GET: Costumer

        [HttpGet]
        public ViewResult Customer()
        {
            var model = songContainer.ReturnAllSongs();
            return View("Customer", model);
        }

        [HttpPost]
        public ViewResult Search(string NameSong)
        {
            song.NameSong = NameSong;
            var model = songContainer.SearchSong(song);
            songViewModel.SongsFound =  "Songs found: " + model.Count.ToString();
            return View("Customer", model);
        }

        public ViewResult Play(string selectedSong)
        {
            ViewBag.SelectedSong = songContainer.GetSelectedSongPath(selectedSong);
            return Customer();
        }
    }
}