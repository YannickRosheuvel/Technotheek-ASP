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
        static UserDAL userDAL;
        Login login;
        User user;
        UserContainer userContainer;

        public AccountController()
        {
            userDAL = new UserDAL();
            login = new Login();
            user = new User();
            userContainer = new UserContainer(userDAL);
        }

        public ActionResult Login()
        {
            HttpContext.Session.GetString("ID");
            return View();
        }

        public ActionResult RegisterUser(RegisterViewModel user)
        {
            //try add User voegt de waardes toe aan de database
            if (ModelState.IsValid)
            {
                try
                {
                    userContainer.AddUser(user);
                    ViewBag.SuccesOrNot = "You succesfully created an account!";
                    return RedirectToAction("Login", "Account");
                }
                catch
                {
                    ViewBag.SuccesOrNot = "Your account could not be created!";
                    return RedirectToAction("Register", "Account");
                }
            }
            ViewBag.SuccesOrNot = "Please enter all required fields!";
            return RedirectToAction("Register", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

        //Verified de user (kijkt in de database of de gegeven parameters matchen) 
        //en haalt vervolgens de info op en zet ze in een session
        public ActionResult Verify(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                user = userContainer.LoginUser(model);

                if(user.Username != null)
                {
                HttpContext.Session.SetString("ID", user.ID.ToString());
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("LastName", user.LastName);
                HttpContext.Session.SetString("Email", user.Username);
                HttpContext.Session.SetString("Street", user.Street);
                HttpContext.Session.SetString("StreetNmr", user.StreetNmr.ToString());
                HttpContext.Session.SetString("City", user.City.ToString());
                HttpContext.Session.SetString("Contact", user.Contact.ToString());
                
                HttpContext.Session.SetString("FunctionType", user.FunctionType.ToString());

                if (model.IsAdmin == true)
                {
                    return RedirectToAction("Index", "Admin", user);
                }
                if (model.IsAdmin == false && login.AdminOrCostumer == false)
                {
                    return RedirectToAction("Index", "Customer", user);
                }

                }
                else
                {
                    ViewBag.SuccesOrNot = "Wrong Password";
                }
            }
            return RedirectToAction("Index","Home");
        }
    }
}