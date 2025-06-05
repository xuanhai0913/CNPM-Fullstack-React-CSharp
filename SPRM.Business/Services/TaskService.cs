using SPRM.Business.Interfaces;

namespace SPRM.Business.Services
{
    public class TaskService : ITaskService
    {
        public string MonitorTasks()
        {
            // TODO: Lấy danh sách task cần theo dõi
            return "Danh sách task đang theo dõi";
        }

        public bool EvaluateMilestone(object milestoneDto)
        {
            // TODO: Đánh giá milestone
            return true;
        }
    }
}