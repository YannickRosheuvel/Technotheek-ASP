using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technotheek.net_Core.Models
{
    public class Employee
    {
        public Employee(EmployeeSpace employeeSpace, string Name, FunctionType functionType )
        {
            this.functionType = functionType;
            this.employeeSpace = employeeSpace;
            this.name = name;
        }

        public enum FunctionType
        {
            ProjectManager,
            HardwareEngineer,
            SoftwareEngineer
        }

        public enum EmployeeSpace
        {
            Small = 2,
            Medium = 4,
            Big = 10
        }

        public EmployeeSpace employeeSpace { get; set; }
        public FunctionType functionType { get; set; }
        public bool Added { get; set; }
        public string name { get; set; }

    }
}
