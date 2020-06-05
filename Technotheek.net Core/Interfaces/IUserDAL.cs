﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnotheekWeb.Models;

namespace TechnotheekWeb.Interfaces
{
    public interface IUserDAL
    {
        void Registration(User user);
        User Login(Login bel, string Email, string Password, int userID);
        User GetUserData(int userID);
        void InsertImage(User user, int userID);
        string GetUserPicture(User user, int userID);
    }
}
