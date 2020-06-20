using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technotheek.net_Core.LOGIC;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;

namespace Technotheek.net_Core.Interfaces
{
    interface IBuilding
    {
        List<Employee> SortEmployees(List<Employee> unorderEmployees);
        List<RoomContainer> AddEmployees(List<Room> roomList, List<Employee> employeeList);
        bool NextRoom(List<Room> roomsGiven);
        List<Room> SortRooms(List<Room> unorderdRooms);
        void AddEmployeesToList(List<Employee> employeeList);
        void AddRoomsToList(List<Room> roomList);
        void AddUnaddedEmployees(List<Employee> sortedEmployees, List<Room> roomsGiven);
        List<Employee> GetUnAddedEmployees(List<Employee> sortedEmployees);
        void AddToNextRoom(Employee employee, List<Room> roomsGiven);
    }
}
