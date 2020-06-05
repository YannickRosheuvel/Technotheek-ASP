﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void AddUser(User user)
        {
            userDAL.Registration(user);
        }

        public User LoginUser(Login login, string Email, string Password, int userID)
        {
            return userDAL.Login(login, Email, Password, userID);
        }

        public User RetrieveUserData(int userID)
        {
            return userDAL.GetUserData(userID);
        }

        public void ReturnInsertImage(User user, int userID)
        {
            userDAL.InsertImage(user, userID);
        }

        public string GetPictureUser(User user, int userID)
        {
            return userDAL.GetUserPicture(user, userID);
        }
    }
}
