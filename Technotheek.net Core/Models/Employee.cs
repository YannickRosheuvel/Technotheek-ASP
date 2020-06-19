using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technotheek.net_Core.Models
{
    public class Employee
    {

        public Employee(EmployeeSpace space, string name, FunctionType functionType)
        {
            this.Name = name;
            this.employeeSpace = space;
            this.functionType = functionType;
        }

        public enum EmployeeSpace
        {
            Small = 2,
            Medium = 4,
            Big = 10
        }

        public enum FunctionType
        {
            SoftwareEngineer,
            HardwareEngineer,
            ProjectManager
        }

        public EmployeeSpace employeeSpace { get; set; }
        public string Name { get; set; }
        public FunctionType functionType { get; set; }
        public bool Added { get; set; }
    }
}
