using NexusPilot_Tasks_Service_src.Models;

namespace NexusPilot_Tasks_Service_src.Services.Interfaces
{
    public interface ITaskService
    {
        //Creation methods
        Task<bool> CreateNewTask(string taskOwnerUUID, string projectUUID, string summary, string description, string imageUrl, string priority, DateOnly startDate, DateOnly endDate);
        //Retrieval methods
        Task<(bool isSuccess, List<TaskItem> tasksList)> GetAllTasksForProject(string projectUUID);
        Task<(bool isSuccess, List<TaskItem> taskList)> GetActiveTasksForProject(string projectUUID);
        Task<(bool isSuccess, List<Assignee>? assigneesList)> GetTaskAssignees(string taskUUID);
        //Mutation methods
        Task<bool> AddAssigneeToTask(string taskUUID, string assigneeUUID, string assigneeNickName);
        Task<bool> MarkTaskAsDone(string taskUUID);
        //Deletion methods
        Task<bool> DeleteTask(string taskUUID);

    }
}
