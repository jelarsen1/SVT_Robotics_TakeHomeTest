using System.Text.Json.Serialization;

namespace SVT_Robotics_TakeHomeTest.Models
{
    public class RobotsExternalResponse
    {
        [JsonPropertyName("robotId")]
        public string? RobotId { get; set; }

        [JsonPropertyName("batteryLevel")]
        public int BatteryLevel { get; set; }

        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
