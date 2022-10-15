namespace SVT_Robotics_TakeHomeTest.Models
{
    public class NearestRobotResponse
    {
        public string? RobotId { get; set; }
        public double DistanceToGoal { get; set; }
        public int BatteryLevel { get; set; }
    }
}
