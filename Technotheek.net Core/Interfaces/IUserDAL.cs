using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technotheek.net_Core.ViewModels;
using TechnotheekWeb.Models;

namespace TechnotheekWeb.Interfaces
{
    public interface IUserDAL
    {
        RegisterViewModel Registration(RegisterViewModel user);
        User Login(LoginViewModel model);
        User GetUserData(int userID);
        User InsertImage(User user, int userID);
        string GetUserPicture(int userID);
    }
}
