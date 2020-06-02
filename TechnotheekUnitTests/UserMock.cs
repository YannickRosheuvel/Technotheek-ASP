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
                ID = 1,
                FunctionType = "Admin"
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

        public void GetUserPicture(User user, int userID)
        {
            throw new NotImplementedException();
        }

        public void InsertImage(User user, int userID)
        {
            throw new NotImplementedException();
        }

        public User Login(Login login, string Email, string Password, int userID)
        {

            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].Password == Password && userList[i].Username == Email)
                {
                    if(userList[i].FunctionType == "Admin")
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


        public void Registration(User bel, string Username, string Password, int Contact, string FirstName, string LastName, string Street, int StreetNmr, string City, int userID)
        {
            user.Username = Username;
        }
    }
}
