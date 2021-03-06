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
        static SongDAL songDAL;
        static UserDAL userDAL;
        static PlaylistDAL playlistDAL;
        SongContainer songContainer;
        UserContainer userContainer;
        PlaylistContainer playlistContainer;
        SongViewModel songViewModel;
        Song song;

        static User currentUser;
        static string currentPlaylist;
        static List<Song> currentSongs;

        private readonly IWebHostEnvironment hostingEnviroment;

        //HostingEnviroment word gebruikt voor het storen van fotos
        public CustomerController(IWebHostEnvironment hostingEnviroment)
        {
            songDAL = new SongDAL();
            userDAL = new UserDAL();
            playlistDAL = new PlaylistDAL();
            songContainer = new SongContainer(songDAL);
            userContainer = new UserContainer(userDAL);
            playlistContainer = new PlaylistContainer(playlistDAL);
            songViewModel = new SongViewModel();
            song = new Song();

            this.hostingEnviroment = hostingEnviroment;
        }

        [HttpGet]
        public IActionResult Customer()
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
                return RedirectToAction("Index", "Account");
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

        public IActionResult Play(string selectedSong)
        {
            ViewBag.SelectedSong = songContainer.GetSelectedSongPath(selectedSong);
            ViewBag.SongInfo = songContainer.GetSongInfo(songContainer.GetSelectedSongPath(selectedSong));

            return Customer();
        }

        public IActionResult Playlist()
        {
            if (HttpContext.Session.GetString("ID") != null)
            {
                ViewBag.Playlists = playlistContainer.GetPlaylists(currentUser.ID);
                ViewBag.User = currentUser;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        public IActionResult CreatePlaylist(string PlaylistName)
        {
            playlistContainer.MakeNewPlaylist(PlaylistName, currentUser.ID);
            return RedirectToAction("Playlist", "Customer");
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
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        //Zorgt voor het uploaden van  de profiel foto in een map en geeft de path mee aan de database
        [HttpPost]
        public IActionResult Upload(ProfilePictureViewModel model)
        {
            User user = new User();
            if (ModelState.IsValid)
            {
                var pictureLocation = userContainer.GetPictureUser(currentUser.ID);

                if (pictureLocation != "")
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    System.IO.File.Delete("wwwroot/ProfilePictures/" + pictureLocation);
                }

                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnviroment.WebRootPath, "ProfilePictures");
                    user.PictureLocation = Guid.NewGuid().ToString() + "_" + model.Image.FileName;

                    string filePath = Path.Combine(uploadsFolder, user.PictureLocation);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                    userContainer.ReturnInsertImage(user, currentUser.ID);

                    ViewBag.SuccesOrNot = "Photo Saved!";
                }
                else
                {
                    ViewBag.SuccesOrNot = "ERROR! Photo not saved. ";
                }
            }
            return RedirectToAction("userPage" , "Home");
        }

        //Opent een playlist naar keuze met alle toebehorende nummers die daar in zitten
        public IActionResult Open(int selectedPlaylistID, string selectedPlaylistName)
        {
            ViewBag.User = currentUser;
            ViewBag.Playlist = selectedPlaylistName;

            currentPlaylist = ViewBag.Playlist;

            var model = playlistContainer.RetrievePlaylistSongs(selectedPlaylistID);
            currentSongs = model;

            return View("PlaylistSongs", model);
        }

        public ViewResult PlaySong(string selectedSong)
        {
            ViewBag.Playlist = currentPlaylist;
            ViewBag.SongInfo = songContainer.GetSongInfo(songContainer.GetSelectedSongPath(selectedSong));
            ViewBag.User = currentUser;
            ViewBag.SelectedSong = songContainer.GetSelectedSongPath(selectedSong);

            return View("PlaylistSongs", currentSongs);
        }

        public IActionResult Add(int selectedPlaylist, int selectedSong) 
        {
            PlaylistSongs playlistSongs = new PlaylistSongs();
            try
            {
                playlistSongs.playlistID = selectedPlaylist;
                playlistSongs.songID = selectedSong;
                playlistContainer.AddSongPlaylist(playlistSongs);
                ViewBag.SuccesOrNot = "Song saved to playlist!";
            }
            catch
            {
                ViewBag.SuccesOrNot = "ERROR! Song did not save to playlist";
            }
            
            return Customer();
        }

        [HttpPost]
        public IActionResult AddSongPlaylistPartial(int SongID)
        {
            Song song = new Song();

            song.ID = SongID;

            return View("_AddSongToPlaylistPartial", song);
        }
    }
}