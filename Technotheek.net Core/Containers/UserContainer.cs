using System;
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

        public void AddUser(User register, string Username, string Password, int Contact, string FirstName, string LastName, string Street, int StreetNmr, string City, int userID)
        {
            userDAL.Registration(register, Username, Password, Contact, FirstName, LastName, Street, StreetNmr, City, userID);
        }

        public bool LoginUser(Login login, string Email, string Password, int userID)
        {
            return userDAL.Login(login, Email, Password, userID);
        }

        public void RetrieveUserData(int userID)
        {
            userDAL.GetUserData(userID);
        }
    }
}
