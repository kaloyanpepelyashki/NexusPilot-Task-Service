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
    }
}
