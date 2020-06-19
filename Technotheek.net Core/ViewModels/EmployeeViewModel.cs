using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Technotheek.net_Core.Models.Employee;

namespace Technotheek.net_Core.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeSpace employeeSpace { get; set; }
        public string name { get; set; }
        public FunctionType functionType { get; set; }
    }
}
