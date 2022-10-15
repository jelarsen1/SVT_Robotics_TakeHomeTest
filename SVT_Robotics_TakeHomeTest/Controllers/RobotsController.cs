using Microsoft.AspNetCore.Mvc;
using SVT_Robotics_TakeHomeTest.Models;
using System.Text.Json;

namespace SVT_Robotics_TakeHomeTest.Controllers
{
    [ApiController]
    public class RobotsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public RobotsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost]
        [Route("/api/robots/closest")]
        public async Task<ActionResult<NearestRobotResponse>> GetClosestRobot([FromBody] NearestRobotRequest request)
        {
            var externalEndpoint = "https://60c8ed887dafc90017ffbd56.mockapi.io/robots";
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync(externalEndpoint);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var allRobots = JsonSerializer.Deserialize<List<RobotsExternalResponse>>(jsonResponse, new JsonSerializerOptions { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase });
            var allRobotsConverted = allRobots?.Select(robot => new NearestRobotResponse
            {
                RobotId = robot.RobotId,
                DistanceToGoal = CalculateDistance(robot.X - request.X, robot.Y - request.Y),
                BatteryLevel = robot.BatteryLevel
            }).ToList();
            var nearestRobot = allRobotsConverted?.Where(robot => robot.DistanceToGoal <= 10).OrderByDescending(robot => robot.BatteryLevel).FirstOrDefault();
            if(nearestRobot != null)
            {
                return Ok(nearestRobot);
            }
            else
            {
                return NotFound("No robot found");
            }
        }

        private static double CalculateDistance(double x, double y)
        {
            return (double)Math.Sqrt(x * x + y * y);
        }

    }

}
