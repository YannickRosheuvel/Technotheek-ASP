using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnotheekWeb.Models
{
    /// <summary>
    /// the data class for register
    /// </summary>
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Contact { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public int StreetNmr { get; set; }
        public string City { get; set; }
        public bool Succesfull { get; set; }
    }
}
