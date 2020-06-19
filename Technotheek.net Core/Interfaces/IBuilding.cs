using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.Models.RoomSpace;
using Technotheek.net_Core.ViewModels;

namespace Technotheek.net_Core.Interfaces
{
    interface IBuilding
    {
        List<Employee> SortEmployees(List<Employee> unorderEmployees);
        void AddEmployees(List<Rooms> roomList, List<Employee> employeeList);
        void NextRoom();
        List<Rooms> SortRooms(List<Rooms> unorderdRooms);
        void AddEmployeesToList(List<Employee> employeeList);
        void AddRoomsToList(List<Rooms> roomList);
        void AddUnaddedEmployees(List<Employee> sortedEmployees);
        List<Employee> GetUnAddedEmployees(List<Employee> sortedEmployees);
        void AddToNextRoom(Employee employee);
    }
}
