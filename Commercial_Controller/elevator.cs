using System.Threading;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Elevator
    {
        public int ID;
        public string status;
        public int amountOfFloors;
        public int currentFloor;
        public string direction;
        public bool overweight;
        public List<int> floorRequestsList;
        public List<int> completedRequestsList;

        public  Door door = new Door("closed");
        public Elevator(int _id, string _status, int _amountOfFloors, int _currentFloor)
        {
            ID = _id;
            status = _status;
            amountOfFloors = _amountOfFloors;
            currentFloor = _currentFloor;
            floorRequestsList = new List<int>();
            completedRequestsList = new List<int>();
            direction = null;
            overweight = false;
            
        }
        public void move()
        {
            while (floorRequestsList.Count > 0)
            {
                int destination = floorRequestsList[0];
                int screenDisplay = 0;
                status = "moving";
                if (currentFloor < destination)
                {
                    direction = "up";
                    sortFloorList();
                    while (currentFloor < destination)
                    {
                        currentFloor += 1;
                        screenDisplay = currentFloor;
                    }
                }
                else if(currentFloor > destination)
                {
                    direction = "down";
                    sortFloorList();
                    while (currentFloor > destination)
                    {
                        currentFloor -= 1;
                        screenDisplay = currentFloor;
                    }
                }
                status = "stopped";
                completedRequestsList.Add(floorRequestsList[0]);
                floorRequestsList.RemoveAt(0);
            }
            status = "idle";
        }
        
        public void sortFloorList()
        {
            if (direction == "up")
            {
                floorRequestsList.Sort((a, b) => a.CompareTo(b));
            }
            else 
            {
                floorRequestsList.Sort((a, b) => b.CompareTo(a));
            }
        }

        public void operateDoors()
        {
            door.status = "opened";
        
        }
        public void addNewRequest(int requestedFloor)
        {
            if (!floorRequestsList.Contains(requestedFloor))
            {
                floorRequestsList.Add(requestedFloor);
            }
            if (currentFloor < requestedFloor)
            {
                direction = "up";
            }
            if (currentFloor > requestedFloor)
            {
                direction = "down";
            }
        }
    }
}