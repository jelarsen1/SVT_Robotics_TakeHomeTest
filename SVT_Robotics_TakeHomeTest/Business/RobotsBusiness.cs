using SVT_Robotics_TakeHomeTest.Models;
using System.Text.Json;

namespace SVT_Robotics_TakeHomeTest.Business
{
    public class RobotsBusiness : IRobotsBusiness
    {
        private readonly IHttpClientFactory _clientFactory;

        public RobotsBusiness(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<NearestRobotResponse> FindClosestRobot(NearestRobotRequest request)
        {
            var externalEndpoint = "https://60c8ed887dafc90017ffbd56.mockapi.io/robots";
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(externalEndpoint);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var allRobots = JsonSerializer.Deserialize<List<RobotsExternalResponse>>(jsonResponse);
            var allRobotsConverted = allRobots?.Select(robot => new NearestRobotResponse
            {
                RobotId = robot.RobotId,
                DistanceToGoal = CalculateDistance(robot.X - request.X, robot.Y - request.Y),
                BatteryLevel = robot.BatteryLevel
            }).ToList();
            var nearestRobot = allRobotsConverted?.Where(robot => robot.DistanceToGoal <= 10).OrderByDescending(robot => robot.BatteryLevel).FirstOrDefault();
            return nearestRobot;
        }

        private static double CalculateDistance(double x, double y)
        {
            return (double)Math.Sqrt(x * x + y * y);
        }
    }
}
