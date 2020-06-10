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

        public void AddUser(RegisterViewModel user)
        {
            userDAL.Registration(user);
        }

        public User LoginUser(LoginViewModel model)
        {
            return userDAL.Login(model);
        }

        public User RetrieveUserData(int userID)
        {
            return userDAL.GetUserData(userID);
        }

        public void ReturnInsertImage(User user, int userID)
        {
            userDAL.InsertImage(user, userID);
        }

        public string GetPictureUser(int userID)
        {
            return userDAL.GetUserPicture(userID);
        }
    }
}
