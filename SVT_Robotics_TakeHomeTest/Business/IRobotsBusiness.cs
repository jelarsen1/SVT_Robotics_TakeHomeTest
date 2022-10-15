using SVT_Robotics_TakeHomeTest.Models;

namespace SVT_Robotics_TakeHomeTest.Business
{
    public interface IRobotsBusiness
    {
        Task<NearestRobotResponse> FindClosestRobot(NearestRobotRequest request);
    }
}
