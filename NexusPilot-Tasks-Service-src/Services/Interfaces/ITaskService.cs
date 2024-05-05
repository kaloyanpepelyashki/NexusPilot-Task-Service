namespace NexusPilot_Tasks_Service_src.Services.Interfaces
{
    public interface ITaskService
    {
        Task<bool> CreateNewTask(string taskOwnerUUID, string projectUUID, string summary, string description, string imageUrl, string priority);
    }
}
