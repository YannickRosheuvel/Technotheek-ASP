using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technotheek.net_Core.Interfaces;
using Technotheek.net_Core.Models;
using static Technotheek.net_Core.Models.Room;

namespace Technotheek.net_Core.LOGIC
{
    public class RoomContainer : IRoom
    {
        private int spaceLeftOver;
        private int spaceTaken;

        private List<Employee> employeesInRoom;

        public RoomContainer()
        {
            employeesInRoom = new List<Employee>();
            spaceTaken = 0;
            spaceLeftOver = 0;
        }

        public List<Employee> ReturnEmployees()
        {
            return new List<Employee>(employeesInRoom);
        }

        // Voegt als het mogelijk is een employee aan de kamer toe.
        public List<Employee> AddToRoom(Employee employee, SpaceBuilding roomSpace)
        {
            if(employeesInRoom.Count == 0)
            {
                spaceLeftOver = Convert.ToInt32(roomSpace);
            }

            if (CheckCompatibility(employee, employeesInRoom) && CheckRoom(employee, spaceLeftOver))
            {
                employeesInRoom.Add(employee);
                employee.Added = true;
                return new List<Employee>(employeesInRoom);
            }
            else
            {
                return new List<Employee>(employeesInRoom);
            }
        }

        // Checkt of er nog genoeg bechikbare ruimte in de kamer is
        public bool CheckRoom(Employee employee, int spaceOver)
        {
            if (spaceOver - Convert.ToInt32(employee.employeeSpace) >= 0)
            {
                spaceTaken = spaceTaken + Convert.ToInt32(employee.employeeSpace);
                spaceLeftOver = spaceOver - spaceTaken;
                spaceTaken = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checkt of bepaalde employees wel bij elkaar kunnen
        public bool CheckCompatibility(Employee employeeToCheck, List<Employee> employeesInRoom)
        {
            foreach (Employee employeeInRoom in employeesInRoom)
            {
                if (employeeToCheck.functionType == Models.Employee.FunctionType.ProjectManager && employeeInRoom.functionType == Models.Employee.FunctionType.ProjectManager)
                {
                    return false;
                }
                if (employeeToCheck.functionType == Employee.FunctionType.SoftwareEngineer && employeeInRoom.functionType == Employee.FunctionType.HardwareEngineer)
                {
                    return false;
                }
                if (employeeToCheck.functionType == Employee.FunctionType.HardwareEngineer && employeeInRoom.functionType == Employee.FunctionType.SoftwareEngineer)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
