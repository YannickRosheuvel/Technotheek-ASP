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
        User user = new User();
        UserContainer userContainer = new UserContainer(userDAL);

        public ActionResult Login()
        {
            HttpContext.Session.GetString("ID");
            return View();
        }

        public ActionResult RegisterUser(User user, string ErrorRegistration)
        {
            //try add User voegt de waardes toe aan de database
            try
            {
                userContainer.AddUser(user);
                //pop up conformation
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                ErrorRegistration = "Registration failed" + ex;
                return RedirectToAction("Register", "Account", ex);
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        //Verified de user (kijkt in de database of de gegeven parameters matchen) 
        //en haalt vervolgens de info op en zet ze in een session
        public ActionResult Verify(string Email, string Password, int userID)
        {
            user = userContainer.LoginUser(login, Email, Password, userID);

            HttpContext.Session.SetString("ID", user.ID.ToString());
            HttpContext.Session.SetString("FirstName", user.FirstName);
            HttpContext.Session.SetString("LastName", user.LastName);
            HttpContext.Session.SetString("Email", user.Username);
            HttpContext.Session.SetString("Street", user.Street);
            HttpContext.Session.SetString("StreetNmr", user.StreetNmr.ToString());
            HttpContext.Session.SetString("City", user.City.ToString());
            HttpContext.Session.SetString("Contact", user.Contact.ToString());
            HttpContext.Session.SetString("FunctionType", user.FunctionType.ToString());

            if (login.IsAdmin == true)
            {
                return RedirectToAction("Admin", "Admin", user);
            }
            if(login.IsAdmin == false && login.AdminOrCostumer == false)
            {
                return RedirectToAction("Index", "Customer", user);
            }
            else
            {
                return View("Error");
            }
        }
    }
}