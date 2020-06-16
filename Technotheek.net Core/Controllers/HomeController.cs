﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Technotheek.net_Core.Models;
using TechnotheekWeb.Containers;
using TechnotheekWeb.DAL;
using TechnotheekWeb.Models;

namespace Technotheek.net_Core.Controllers
{
    public class HomeController : Controller
    {
        static UserDAL userDAL = new UserDAL();
        UserContainer userContainer = new UserContainer(userDAL);
        User user = new User();

        static User currentUser;

        const string SessionName = "No name found";

        //De user word mee gegeven met de Index en vervolgens in een static currentUser gestored zodat
        //Zodat de info van de user ter alle tijden in de sessie toegangkelijk is.
        //De CurrentUser Word alleen gezet als hij nog niet gezet is.
        public IActionResult Index(User user)
        {
            if (HttpContext.Session.GetString("ID") != null)
            {
                if(currentUser == null)
                {
                    currentUser = user;
                    ViewBag.User = user;
                    return View();
                }
                else
                {
                    ViewBag.User = currentUser;
                    return View("Index");
                }
            }
            else
            {
                return View();
            }

        }

        public IActionResult UserPage()
        {

            if (HttpContext.Session.GetString("ID") != null)
            {
                ViewBag.Picture = userContainer.GetPictureUser(currentUser.ID);
                ViewBag.User = currentUser;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
