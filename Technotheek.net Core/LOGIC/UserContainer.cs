﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technotheek.net_Core.ViewModels;
using TechnotheekWeb.DAL;
using TechnotheekWeb.Interfaces;
using TechnotheekWeb.Models;

namespace TechnotheekWeb.Containers
{
    public class UserContainer
    {
        IUserDAL userDAL;

        public UserContainer(IUserDAL userDAL)
        {
            this.userDAL = userDAL;
        }

        public RegisterViewModel AddUser(RegisterViewModel user)
        {
            try
            {
                return userDAL.Registration(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public User LoginUser(LoginViewModel model)
        {
            try
            {
                return userDAL.Login(model);
            }
            catch
            {
                throw new Exception("Login failed, Please check your internet connection");
            }
        }

        public User RetrieveUserData(int userID)
        {
            try
            {
                return userDAL.GetUserData(userID);
            }
            catch
            {
                throw new Exception("Error! Could not retrieve User Data.");
            }
        }

        public User ReturnInsertImage(User user, int userID)
        {
            try
            {
                return userDAL.InsertImage(user, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public string GetPictureUser(int userID)
        {
            try
            {
                return userDAL.GetUserPicture(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public string Reccomendations(int userID)
        {
            try
            {
                return userDAL.GetUserPicture(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
