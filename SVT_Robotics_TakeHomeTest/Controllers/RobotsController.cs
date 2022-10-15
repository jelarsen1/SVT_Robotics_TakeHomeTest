using Microsoft.AspNetCore.Mvc;
using SVT_Robotics_TakeHomeTest.Models;
using SVT_Robotics_TakeHomeTest.Business;

namespace SVT_Robotics_TakeHomeTest.Controllers
{
    [ApiController]
    public class RobotsController : Controller
    {
        private static IRobotsBusiness? _robotsBusiness;
        public RobotsController(IRobotsBusiness robotsBusiness)
        {
            _robotsBusiness = robotsBusiness;
        }

        [HttpPost]
        [Route("/api/robots/closest")]
        public async Task<ActionResult<NearestRobotResponse>> FindClosestRobot([FromBody] NearestRobotRequest request)
        {
            var nearestRobot = await _robotsBusiness.FindClosestRobot(request);

            if (nearestRobot != null)
            {
                return Ok(nearestRobot);
            }
            else
            {
                return NotFound("No robot found");
            }
        }

    } 

}
