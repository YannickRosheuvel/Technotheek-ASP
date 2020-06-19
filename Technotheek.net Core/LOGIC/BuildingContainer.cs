using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technotheek.net_Core.Models;
using Technotheek.net_Core.ViewModels;
using Technotheek.net_Core.Models.RoomSpace;
using Technotheek.net_Core.Interfaces;
using static Technotheek.net_Core.Models.RoomSpace.Room;

namespace Technotheek.net_Core.LOGIC
{
    public class BuildingContainer : IBuilding
    {
        private List<Employee> givenEmployees;
        private List<Room> givenRooms;
        private List<RoomContainer> listRooms;
        private List<Employee> unAddedEmployees;

        int i = 0;

        public BuildingContainer()
        {
            givenRooms = new List<Room>();
            givenEmployees = new List<Employee>();
            listRooms = new List<RoomContainer>();
            unAddedEmployees = new List<Employee>();
        }

        public List<RoomContainer> ReturnRooms()
        {
            return new List<RoomContainer>(listRooms);
        }

        //Zet de projectleiders bovenaan en orderd daarna op grootte van groot naar klein.
        public List<Employee> SortEmployees(List<Employee> unorderdEmployees)
        {
            return unorderdEmployees.OrderByDescending(employee => employee.functionType)
                .ThenByDescending(employee => (int)employee.employeeSpace)
                .ToList();
        }

        //Zet de kamers van groot naar klein.
        public List<Room> SortRooms(List<Room> unorderdRooms)
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

        public void AddRoomsToList(List<Room> roomList)
        {
            foreach (Room item in roomList)
            {
                //Amount of rooms added
                RoomContainer roomContainer = new RoomContainer();
                listRooms.Add(roomContainer);
                //The size of room added for every room
                Room newRoom = new Room(item.buildingSpace);
                givenRooms.Add(newRoom);
            }
        }

        // De main functie die alle andere functies oproept om zo veel mogelijk employees toe te voegen.
        public List<RoomContainer> AddEmployees(List<Room> roomList, List<Employee> employeeList)
        {
            AddEmployeesToList(employeeList);

            AddRoomsToList(roomList);

            List<Employee> sortedEmployees = SortEmployees(givenEmployees);

            List<Room> sortedRooms = SortRooms(givenRooms);

            //Stopt eerst de projectleiders en daarna de grootste in een kamer.
            foreach (Employee employee in sortedEmployees)
            {
                listRooms[i].AddToRoom(employee, sortedRooms[i].buildingSpace);

                AddToNextRoom(employee, givenRooms);
            }

            AddUnaddedEmployees(sortedEmployees, sortedRooms);

            return new List<RoomContainer>(listRooms);
        }

        // Voegt de employees toe die nog niet zijn toegevoegd.
        public void AddUnaddedEmployees(List<Employee> sortedEmployees, List<Room> roomsGiven)
        {
            foreach (var employee in sortedEmployees)
            {
                if (employee.Added == false)
                {
                    foreach (RoomContainer listRoom in listRooms)
                    {
                        int ii = 0;
                        if (ii + 1 < roomsGiven.Count)
                        {
                            listRoom.AddToRoom(employee, roomsGiven[i].buildingSpace);
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
                }
            }
            return new List<Employee>(unAddedEmployees);
        }

        public void AddToNextRoom(Employee employee, List<Room> roomsGiven)
        {
            if (!employee.Added)
            {
                NextRoom(roomsGiven);
                listRooms[i].AddToRoom(employee, roomsGiven[i].buildingSpace);
            }
        }

        // Zorgt ervoor dat de volgende kamer kan worden gevuld.
        public bool NextRoom(List<Room> roomsGiven)
        {
            if (roomsGiven != null)
            {
                if (i + 1 < roomsGiven.Count)
                {
                    i = i + 1;
                    return true;
                }
            }
            return false;

        }
    }
}
