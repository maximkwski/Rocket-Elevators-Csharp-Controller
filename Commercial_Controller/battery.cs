using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Battery
    {
        public int ID;
        public int amountOfColumns;
        public int amountOfFloors;
        public int amountOfBasements;
        public int amountOfElevatorPerColumn;
        public string status;
        public List<Column> columnsList;
        public List<FloorRequestButton> floorRequestsButtonsList;

        public int columnID = 1;
        public int floorRequestButtonID = 1;
        
        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
           ID = _ID;
           amountOfColumns = _amountOfColumns;
           amountOfFloors = _amountOfFloors;
           amountOfBasements = _amountOfBasements;
           amountOfElevatorPerColumn = _amountOfElevatorPerColumn;
           status = "online";
           columnsList = new List<Column>();
           floorRequestsButtonsList = new List<FloorRequestButton>();

           createFloorRequestButtons(_amountOfFloors);
           createColumns(_amountOfColumns, _amountOfFloors, _amountOfBasements, _amountOfElevatorPerColumn);
           if (_amountOfBasements > 0)
           {
             createBasementFloorRequestButtons(_amountOfBasements);
             createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
             _amountOfColumns -= 1;
           }
        }
        
        public void createBasementColumn(int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            List<int> servedFloors = new List<int>();
            int floor = -1;
            
            for (int i = 0; i < _amountOfBasements; i++)
            {
                servedFloors.Add(floor);
                floor -=1;
            }
            Column column = new Column(columnID, _amountOfBasements, _amountOfElevatorPerColumn, servedFloors, true);
            columnsList.Add(column);
            columnID++;
        }

        public void createColumns(int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
           int amountOfFloorsPerColumn = (int)Math.Ceiling((double)(_amountOfFloors / _amountOfColumns));
           int floor = 1;
        //    _amountOfFloors += _amountOfBasements;
        //     int amountOfFloorsPerColumn = 0;
           for (int i = 0; i < _amountOfColumns; i++)
           {
               List<int> servedFloors = new List<int>();
            //    if (i == 0)
            //    {
            //         amountOfFloorsPerColumn = _amountOfBasements + 1;
            //    }
            //    else if (i == 1)
            //    {
            //        amountOfFloorsPerColumn = 19;
            //    }
            //    else
            //    {
            //        amountOfFloorsPerColumn = 20;
            //    }
               
               for(int j = 0; j < amountOfFloorsPerColumn; j++)
               {
                if(floor <= _amountOfFloors)
                {
                    servedFloors.Add(floor);
                    floor +=1 ;
                }
               }
              Column column = new Column(columnID, _amountOfFloors, _amountOfElevatorPerColumn, servedFloors, false);
              columnsList.Add(column);
              columnID++;
           }
        }

        public void createFloorRequestButtons(int _amountOfFloors) //Button on the pannel at the lobby to request any floor
        {   
            int buttonFloor = 1;
            for (int i = 0; i < _amountOfFloors; i++)
            {
                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, "off", buttonFloor,  "up");
                floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor +=1;
                floorRequestButtonID +=1;
            }
        }
 
        public void createBasementFloorRequestButtons(int _amountOfBasements) //Button on the pannel at the lobby to request any floor
        {
            int buttonFloor = -1;
            for (int i = 0; i < _amountOfBasements; i++)
            {
                FloorRequestButton floorRequestButton = new FloorRequestButton(floorRequestButtonID, "off", buttonFloor,  "down");
                floorRequestsButtonsList.Add(floorRequestButton);
                buttonFloor -=1;
                floorRequestButtonID += 1;
            }
        }

        public Column findBestColumn(int _requestedFloor)
        {   
            Column selectedColumn = columnsList[0];
            foreach (Column column in columnsList)
            {
                if(column.servedFloors.Contains(_requestedFloor))
                {
                    selectedColumn = column;
                }
            }
            return selectedColumn;
        }
        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            Column column = findBestColumn(_requestedFloor);
            Elevator elevator = column.findElevator(1, _direction); 
            elevator.addNewRequest(1);
            elevator.move();

            elevator.addNewRequest(_requestedFloor);
            elevator.move();

            return (column, elevator);
             
        }
    }
}

