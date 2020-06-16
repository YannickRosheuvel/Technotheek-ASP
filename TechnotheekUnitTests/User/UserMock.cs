using System;
using System.Collections.Generic;
using System.Text;
using Technotheek.net_Core.ViewModels;
using TechnotheekWeb.Interfaces;
using TechnotheekWeb.Models;

namespace TechnotheekUnitTests
{
    class UserMock : IUserDAL
    {
        public User user = new User();

        public RegisterViewModel registerViewModel = new RegisterViewModel();

        public List<User> userList = new List<User>()
        {
            new User
            {
                Username = "yannick.rosheuvel@gmail.com",
                Password = "123",
                ID = 1,
                FunctionType = "Admin",
                PictureLocation = "pictureLocation"
            }
        };


        public User GetUserData(int userID)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].ID == userID)
                {
                    user.Username = userList[i].Username;
                    return user;
                }
            }
            return new User();
        }

        public string GetUserPicture(int userID)
        {
            foreach(var item in userList)
            {
                if(item.ID == userID)
                {
                    return item.PictureLocation;
                }
            }
            return "";
        }

        public User InsertImage(User user, int userID)
        {
            return user;
        }

        public User Login(LoginViewModel model)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].Password == model.Password && userList[i].Username == model.Email)
                {
                    if (userList[i].FunctionType == "Admin")
                    {
                        return userList[i];
                    }
                    if (userList[i].FunctionType == "Customer")
                    {
                        return userList[i];
                    }
                }
            }
            return user;
        }

        public RegisterViewModel Registration(RegisterViewModel user)
        {
            return user;
        }
    }
}
