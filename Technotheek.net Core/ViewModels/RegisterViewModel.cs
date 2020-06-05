using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Technotheek.net_Core.ViewModels
{
    public class RegisterViewModel
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
    }
}
