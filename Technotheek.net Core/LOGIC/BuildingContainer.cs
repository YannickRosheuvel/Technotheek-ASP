using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using Technotheek.net_Core.Models.RoomSpace;
using Technotheek.net_Core.Interfaces;
using static Technotheek.net_Core.Models.RoomSpace.Rooms;

namespace Technotheek.net_Core.LOGIC
{
    public class BuildingContainer : IBuilding
    {
        private List<Employee> givenEmployees;
        private List<Rooms> givenRooms;
        private List<RoomContainer> emptyListRoom;
        private List<RoomContainer> addedListRoom;
        private List<Employee> unAddedEmployees;

        int i = 0;

        public BuildingContainer()
        {
            givenEmployees = new List<Employee>();
            givenRooms = new List<Rooms>();
            emptyListRoom = new List<RoomContainer>();
            addedListRoom = new List<RoomContainer>();
            unAddedEmployees = new List<Employee>();
        }

        public List<RoomContainer> ReturnRooms()
        {
            return new List<RoomContainer>(emptyListRoom);
        }

        public List<Employee> SortEmployees(List<Employee> unorderdEmployees)
        {
            return unorderdEmployees.OrderByDescending(employee => employee.functionType)
                .ThenByDescending(employee => (int)employee.employeeSpace)
                .ToList();
        }

        public List<Rooms> SortRooms(List<Rooms> unorderdRooms)
        {
            return unorderdRooms.OrderByDescending(building => (int)building.buildingSpace).ToList();
        }

        public void AddEmployeesToList(List<Employee> employeeList)
        {
            foreach (Employee item in employeeList)
            {
                Employee employee = new Employee(item.employeeSpace, item.Name, item.functionType);
                givenEmployees.Add(employee);
            }
        }

        public void AddRoomsToList(List<Rooms> roomList)
        {
            foreach (Rooms item in roomList)
            {
                Rooms newRoom = new Rooms(item.buildingSpace);
                givenRooms.Add(newRoom);
                RoomContainer roomContainer = new RoomContainer();
                addedListRoom.Add(roomContainer);
                emptyListRoom.Add(roomContainer);
            }
        }

        public void AddEmployees(List<Rooms> roomList, List<Employee> employeeList)
        {
            AddEmployeesToList(employeeList);

            AddRoomsToList(roomList);

            List<Employee> sortedEmployees = SortEmployees(givenEmployees);

            List<Rooms> sortedRooms = SortRooms(givenRooms);

            foreach (Employee employee in sortedEmployees)
            {
                addedListRoom[i].AddToRoom(employee, sortedRooms[i].buildingSpace);

                if (givenEmployees.LastOrDefault().Equals(employee))
                {
                    emptyListRoom[i] = addedListRoom[i];
                }

                AddToNextRoom(employee);
            }

            AddUnaddedEmployees(sortedEmployees);
        }

        public void AddUnaddedEmployees(List<Employee> sortedEmployees)
        {
            foreach (var employee in sortedEmployees)
            {
                if (employee.Added == false)
                {
                    foreach (var listRoom in addedListRoom)
                    {
                        int ii = 0;
                        if (ii + 1 < givenRooms.Count)
                        {
                            listRoom.AddToRoom(employee, givenRooms[i].buildingSpace);
                            if(employee.Added == true)
                            {
                                emptyListRoom[i] = addedListRoom[i];
                            }
                            ii = ii + 1;
                        }

                    }
                }
            }

        }

        public List<Employee> GetUnAddedEmployees(List<Employee> sortedEmployees)
        {
            foreach (var employee in sortedEmployees)
            {
                if(employee.Added == false)
                {
                    unAddedEmployees.Add(employee);
                    return new List<Employee>(unAddedEmployees);
                }
            }
            return new List<Employee>(unAddedEmployees);
        }

        public void AddToNextRoom(Employee employee)
        {
            if (!employee.Added)
            {
                emptyListRoom[i] = addedListRoom[i];
                NextRoom();
                addedListRoom[i].AddToRoom(employee, givenRooms[i].buildingSpace);
            }
        }

        public void NextRoom()
        {
            if (givenRooms != null)
            {
                if (i + 1 < givenRooms.Count)
                {
                    i = i + 1;
                }
            }

        }
    }
}
