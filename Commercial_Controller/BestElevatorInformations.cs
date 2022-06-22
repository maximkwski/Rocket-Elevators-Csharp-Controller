namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class BestElevatorInformations
    {
        public Elevator bestElevator;
        public int bestScore;
        public int referenceGap;
        public BestElevatorInformations()
        {
            bestElevator = null;
            bestScore = 6;
            referenceGap = 10000000;
        }
    }
}    