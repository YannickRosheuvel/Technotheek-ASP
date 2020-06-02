using System;
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
        private readonly ILogger<HomeController> _logger;
        User user = new User();

        static User currentUser;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        const string SessionName = "No name found";

        public IActionResult Index(User user)
        {
            if (HttpContext.Session.GetString("ID") != null)
            {
                currentUser = user;
                ViewBag.User = user;
                return View();
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
                ViewBag.Picture = userContainer.GetPictureUser(user, currentUser.ID);
                ViewBag.User = currentUser;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
