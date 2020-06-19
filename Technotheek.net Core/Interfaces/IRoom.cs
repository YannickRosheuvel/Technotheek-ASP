using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using static Technotheek.net_Core.Models.RoomSpace.Room;

namespace Technotheek.net_Core.Interfaces
{
    interface IRoom
    {
        List<Employee> AddToRoom(Employee employee, SpaceBuilding roomSpace);
        bool CheckRoom(Employee employee, int spaceOver);
        bool CheckCompatibility(Employee employeeToCheck, List<Employee> employeesInRoom);

    }
}
