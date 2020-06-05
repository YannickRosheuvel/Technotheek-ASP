using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public int ID { get; set; }
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parameters do not match")]
        public string ComparePassword { get; set; }
        [Required]
        public int Contact { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public int StreetNmr { get; set; }
        [Required]
        public string City { get; set; }
        public bool Succesfull { get; set; }
        public string PictureLocation { get; set; }
        public string FunctionType { get; set; }
    }
}
