using System;
using System.Collections.Generic;
using System.Text;
using TechnotheekWeb.Interfaces;
using TechnotheekWeb.Models;

namespace TechnotheekUnitTests
{
    class UserMock : IUserDAL
    {
        public User user = new User();

        public List<User> userList = new List<User>()
        {
            new User
            {
                Username = "Yoda",
                Password = "123",
                ID = 1
            }
        };


        public void GetUserData(int userID)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].ID == userID)
                {
                    user.Username = userList[i].Username;
                }
            }
        }

        public bool Login(Login bel, string Email, string Password, int userID)
        {
            user.Username = "Yoda";
            user.Password = "123";

            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].Password == user.Password && userList[i].Username == user.Username)
                {
                    return true;
                }
            }
            return false;
        }


        public void Registration(User bel, string Username, string Password, int Contact, string FirstName, string LastName, string Street, int StreetNmr, string City, int userID)
        {
            user.Username = Username;
        }
    }
}
