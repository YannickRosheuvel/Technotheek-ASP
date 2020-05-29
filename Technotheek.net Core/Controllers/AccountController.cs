using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using TechnotheekWeb.Containers;
using TechnotheekWeb.DAL;
using TechnotheekWeb.Models;

namespace TechnotheekWeb.Controllers
{
    public class AccountController : Controller
    {
        static UserDAL userDAL = new UserDAL();
        Login login = new Login();
        User register = new User();
        UserContainer userContainer = new UserContainer(userDAL);
        static SongDAL songDAL = new SongDAL();
        SongContainer songContainer = new SongContainer(songDAL);
        CustomerController customerController = new CustomerController();
        SongCreateViewModel song = new SongCreateViewModel();
        // GET: Account



        [HttpGet]
        public ActionResult Login()
        {
            HttpContext.Session.GetString("ID");
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(string ErrorRegistration, string Email, string FirstName, string LastName, string Password, string RepeatPassword, string City, string Street, int StreetNumber, int PhoneNumber, int userID)
        {
            //try add User voegt de waardes toe aan de database
            try
            {
                userContainer.AddUser(register, Email, Password, PhoneNumber, FirstName, LastName, Street, StreetNumber, City, userID);
                //pop up conformation
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                ErrorRegistration = "Registration failed" + ex;
                return RedirectToAction("Register", "Account");
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            HttpContext.Session.GetString("ID");
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(string Email, string Password, int userID)
        {
            User user = new User();

            userContainer.LoginUser(login, Email, Password, userID);

            if (login.AdminOrNot == true)
            {
                user = userContainer.LoginUser(login, Email, Password, userID);

                HttpContext.Session.SetString("ID", user.ID.ToString());
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("LastName", user.LastName);
                HttpContext.Session.SetString("Email", user.Username);
                HttpContext.Session.SetString("Street", user.Street);
                HttpContext.Session.SetString("StreetNmr", user.StreetNmr.ToString());

                return RedirectToAction("Admin", "Admin");
            }
            if(login.AdminOrNot == false && login.AdminOrCostumer == false)
            {
                user = userContainer.LoginUser(login, Email, Password, userID);

                HttpContext.Session.SetString("ID", user.ID.ToString());
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("LastName", user.LastName);
                HttpContext.Session.SetString("Email", user.Username);
                HttpContext.Session.SetString("Street", user.Street);
                HttpContext.Session.SetString("StreetNmr", user.StreetNmr.ToString());

                var model = songContainer.ReturnAllSongs();
                return RedirectToAction("Index", "Home", model);
            }
            else
            {
                return View("Error");
            }
        }

    }
}