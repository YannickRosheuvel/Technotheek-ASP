﻿using System;
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
using Technotheek.net_Core.Containers;
using Technotheek.net_Core.DAL;
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
        static PlaylistDAL playlistDAL = new PlaylistDAL();
        SongContainer songContainer = new SongContainer(songDAL);
        UserContainer userContainer = new UserContainer(userDAL);
        PlaylistContainer playlistContainer = new PlaylistContainer(playlistDAL);
        SongViewModel songViewModel = new SongViewModel();
        Song song = new Song();
        User user = new User();
        // GET: Costumer

        static User currentUser;

        static List<Song> currentSongs;

        private readonly IWebHostEnvironment hostingEnviroment;

        //HostingEnviroment word gebruikt voor het storen van fotos
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
                ViewBag.Playlists = playlistContainer.GetPlaylists(currentUser.ID);
                return View("Discover", model);
                //return View();
            }
            else
            {
                return View("Login", "Account");
            }
        }

        [HttpPost]
        public ViewResult Search(string NameSong)
        {
            song.Name = NameSong;
            var model = songContainer.SearchSong(song);
            songViewModel.SongsFound =  "Songs found: " + model.Count.ToString();
            return View("Discover", model);
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
                ViewBag.Playlists = playlistContainer.GetPlaylists(currentUser.ID);
                ViewBag.User = currentUser;
                return View();
            }
            else
            {
                return View("Login", "Account");
            }
        }

        public IActionResult CreatePlaylist(string PlaylistName)
        {
            playlistContainer.MakeNewPlaylist(PlaylistName, currentUser.ID);
            return RedirectToAction("Playlist", "Customer");
        }

        public ViewResult AddSongToPlaylist(string songToAdd)
        {
            return Customer();
        }

        //De user word mee gegeven met de Index en vervolgens in een static currentUser gestored zodat
        //Zodat de info van de user ter alle tijden in de sessie toegangkelijk is.
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
                return RedirectToAction("Login", "Account");
            }
        }

        //Zorgt voor het uploaden van  de profiel foto in een map en geeft de path mee aan de database
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

        //Opent een playlist naar keuze met alle toebehorende nummers die daar in zitten
        public IActionResult Open(string selectedPlaylist)
        {
            ViewBag.User = currentUser;
            ViewBag.Playlist = selectedPlaylist;
            var model = playlistContainer.RetrievePlaylistSongs(selectedPlaylist);
            currentSongs = model;
            return View("PlaylistSongs", model);
        }

        public ViewResult PlaySong(string selectedSong)
        {
            ViewBag.User = currentUser;
            ViewBag.SelectedSong = songContainer.GetSelectedSongPath(selectedSong);
            return View("PlaylistSongs", currentSongs);
        }

        public IActionResult Add(string selectedPlaylist)
        {

            return Customer();
        }
    }
}