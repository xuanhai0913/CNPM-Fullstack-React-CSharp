namespace SPRM.Business.Interfaces
{
    public interface ITaskService
    {
        string MonitorTasks();
        bool EvaluateMilestone(object milestoneDto);
    }
}
