namespace Commercial_Controller
{
    //Button on the pannel at the lobby to request any floor
    public class FloorRequestButton
    {
        public int ID;
        public string status;
        public int floor;
        public string direction;
        public FloorRequestButton(int _id, string _status, int _floor, string _direction)
        {
            ID = _id;
            status = _status;
            floor = _floor;
            direction = _direction;
        }
    }
}