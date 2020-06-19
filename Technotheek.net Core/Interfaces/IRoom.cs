using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using static Technotheek.net_Core.Models.RoomSpace.Rooms;

namespace Technotheek.net_Core.Interfaces
{
    interface IRoom
    {
        List<Employee> AddToRoom(Employee employee, SpaceBuilding roomSpace);
        bool CheckRoom(Employee employee);
        bool CheckCompatibility(Employee employeeToCheck);

    }
}
