using NexusPilot_Tasks_Service_src.Models;

namespace NexusPilot_Tasks_Service_src.Services.Interfaces
{
    public interface ITaskService
    {
        Task<bool> CreateNewTask(string taskOwnerUUID, string projectUUID, string summary, string description, string imageUrl, string priority);
        Task<(bool isSuccess, List<TaskItem> tasksList)> GetAllTasksForProject(string projectUUID);
        Task<bool> DeleteTask(string taskId);
    }
}
