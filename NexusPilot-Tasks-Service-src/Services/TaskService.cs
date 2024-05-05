using NexusPilot_Tasks_Service_src.DAO;
using NexusPilot_Tasks_Service_src.Models;
using NexusPilot_Tasks_Service_src.Services.Interfaces;
using Supabase;

namespace NexusPilot_Tasks_Service_src.Services
{
    public class TaskService: ITaskService
    {
        private static TaskService _instance;
        protected SupabaseClient supabaseClient;
        protected Client supabase;

        private TaskService()
        {
            supabaseClient = new SupabaseClient();
            supabase = supabaseClient.SupabaseAccessor;
        }

        public static TaskService GetInstance()
        {
            if(_instance == null)
            {
                _instance = new TaskService();
            }

            return _instance;
        }

        public async Task<bool> CreateNewTask(string taskOwnerUUID, string projectUUID, string summary, string description, string imageUrl, string priority)
        {
            try
            {
                var taskOwnerGuid = new  Guid(taskOwnerUUID);
                var projectGuid = new Guid(projectUUID);

                TaskItem newTask = new TaskItem { Summary = summary, Description = description, ImageUrl = imageUrl, ProjectId = projectGuid, TaskOwnerId = taskOwnerGuid, Pirority = priority };

                var result = await supabase.From<TaskItem>().Insert(newTask);

                if(result != null)
                {
                    return true;
                }

                return false;   

            }catch(Exception e)
            {
                Console.WriteLine($"Error inserting task: {e}");
                throw new Exception($"{e}");
            }
        }

        public async Task<(bool isSuccess, List<TaskItem> tasksList)> GetAllTasksForProject(string projectUUID)
        {
            try
            {
                var projectGuid = new Guid(projectUUID);

                var result = await supabase.From<TaskItem>().Where(task => task.ProjectId == projectGuid).Get();

                if(result != null)
                {
                    List<TaskItem> returnedTasks = new List<TaskItem>();

                    result.Models.ForEach(task =>
                    {
                        returnedTasks.Add(new TaskItem { Id = task.Id, Summary = task.Summary, Description = task.Description, ImageUrl = task.ImageUrl, Pirority = task.Pirority, ProjectId = projectGuid, TaskOwnerId = task.TaskOwnerId });
                    });

                    if(returnedTasks.Count > 0)
                    {
                        return (true, returnedTasks);
                    }

                    return (true, new List<TaskItem>());
                }

                return (false, new List<TaskItem>());

                
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //To do: Implement some validation of completion and error handling
        public async Task<bool> DeleteTask(string taskId)
        {
            try
            {
                

                var result = supabase.From<TaskItem>().Where(task => task.Id == taskId).Delete();

                Console.WriteLine($"Result: {result}");
                return true;

            } catch(Exception e)
            {
                Console.WriteLine($"Error deleting task: {e}");
                throw;
            }
        }
    }
}
