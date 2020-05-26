using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnotheekWeb.Models
{
    /// <summary>
    /// the data class for Login
    /// </summary>
    public class Login
    {
        public string Message { get; set; }
        public bool AdminOrNot { get; set; }
        public bool AdminOrCostumer { get; set; }
    }
}
