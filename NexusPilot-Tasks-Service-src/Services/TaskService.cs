using NexusPilot_Tasks_Service_src.DAO;
using NexusPilot_Tasks_Service_src.Models;
using NexusPilot_Tasks_Service_src.Models.ExceptionModels;
using NexusPilot_Tasks_Service_src.Services.Interfaces;
using Supabase;

namespace NexusPilot_Tasks_Service_src.Services
{
    //Handles all interactions with the Task Entity
    public class TaskService: ITaskService
    {
        private static TaskService _instance;
        protected SupabaseClient _supabaseClient;
        protected Client supabase;

        //Supabase Client DAO is dependency injected into the service
        public TaskService(SupabaseClient supabaseClient)
        {
            _supabaseClient = supabaseClient;
            supabase = _supabaseClient.SupabaseAccessor;
        }


        /** This method is in charge of creating a new task
         * The method inserts a new record in the tasks table in the database */
        public async Task<bool> CreateNewTask(string taskOwnerUUID, string projectUUID, string summary, string description, string imageUrl, string priority, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                var taskOwnerGuid = new  Guid(taskOwnerUUID);
                var projectGuid = new Guid(projectUUID);

                TaskItem newTask = new TaskItem { Summary = summary, Description = description, ImageUrl = imageUrl, ProjectId = projectGuid, TaskOwnerId = taskOwnerGuid, Pirority = priority, StartDate = startDate, EndDate = endDate };

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

        /* This method checks if the assignee is already assigned to the task
         * If the assignee is already assigned to the task, it returns false, as the operation to re-assign would be invalid
         * If no records matching the query are returned, the method returns true, as the assignee can be assigned to this task.
         */
        protected async Task<bool> CheckTaskToAssigneeIsValid(Guid taskGuid, Guid assigneeGuid)
        {
            try
            {

                var result = await supabase.From<Assignee>().Where(item => item.TaskId == taskGuid && item.AssigneeId == assigneeGuid).Get();

                if(result != null)
                {
                    if(result.Models.Count > 0)
                    {
                        return false;
                    }
                }

                return true;

            } catch(Exception e)
            {
                throw;
            }
        }

        /** This method is in charge of adding an assignee to a task
         * The method inserts a new record in the junction table between tasks and user_accounts "taskassignees" */
        public async Task<bool> AddAssigneeToTask(string taskUUID, string assigneeUUID, string assigneeNickName)
        {
            try
            {
                var taskGuid = new Guid(taskUUID);
                var assigneeGuid = new Guid(assigneeUUID);

                var operationIsValid = await CheckTaskToAssigneeIsValid(taskGuid, assigneeGuid);

                if (operationIsValid)
                {

                    Assignee newAssignee = new Assignee { AssigneeId = assigneeGuid, TaskId = taskGuid, AssigneeNickName = assigneeNickName };

                    var result = await supabase.From<Assignee>().Insert(newAssignee);

                    if (result != null)
                    {
                        return true;
                    }

                    return false;

                } else
                { 
                
                    //In case the operationIsValid is false, the user is already an assignee on the task. Therefore the system throws an exception
                    throw new AlreadyExistsException("User already assigned to this task");
                }

            } catch(Exception e)
            {
                throw;
            }

        }
        /** This method is in charge of fetching all tasks belonging to a project
         The method queries the database and fetches all records that satisfy the criteria */
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
                        returnedTasks.Add(new TaskItem { Id = task.Id, Summary = task.Summary, Description = task.Description, ImageUrl = task.ImageUrl, Pirority = task.Pirority, EndDate = task.EndDate, StartDate = task.StartDate, Done = task.Done, ProjectId = projectGuid, TaskOwnerId = task.TaskOwnerId });
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

        /** This method is in charge of fetching all ACTIVE tasks belonging to a project
         The method queries the database and fetches all records that satisfy the criteria */
        public async Task<(bool isSuccess, List<TaskItem> taskList)> GetActiveTasksForProject(string projectUUID)
        {
            try
            {
                var projectGuid = new Guid(projectUUID);

                var result = await supabase.From<TaskItem>().Where(task => task.ProjectId == projectGuid && task.Done == false).Get();

                if(result.ResponseMessage.IsSuccessStatusCode)
                {
                    if (result.Models.Count > 0)
                    {
                        List <TaskItem> returnedTasks = new List<TaskItem>();

                        result.Models.ForEach(task =>
                        {
                            returnedTasks.Add(new TaskItem { Summary = task.Summary, Description = task.Description, ImageUrl = task.ImageUrl, Pirority = task.Pirority, ProjectId = projectGuid, EndDate = task.EndDate, StartDate = task.StartDate, Done = task.Done, TaskOwnerId = task.TaskOwnerId });
                        });

                        return (true, returnedTasks);
                    }

                    return (true, new List<TaskItem>());
                }

                return (false, new List<TaskItem>());

            } catch(Exception e)
            {
                throw;
            }
        }

        /*This method queries the taskassignees table and returns all records, associated with the taskId passed as parameter */
        public async Task<(bool isSuccess, List<Assignee>? assigneesList)> GetTaskAssignees(string taskUUID)
        {
            try
            {
                var taskGuid = new Guid(taskUUID);

                var result = await supabase.From<Assignee>().Where(item => item.TaskId == taskGuid).Get();

                if(result != null)
                {
                    List<Assignee> assigneesList = new List<Assignee>();

                    result.Models.ForEach(assignee =>
                    {
                        assigneesList.Add(new Assignee { TaskId = assignee.TaskId, AssigneeId = assignee.AssigneeId, AssigneeNickName = assignee.AssigneeNickName, });
                    });


                    return (true, assigneesList);
                    
                }

                return (false, new List<Assignee>());

            } catch(Exception e)
            {
                throw;
            }
        }
        /** This method is in charge of changing the state of a task to "done"
        The method queries the database and modifies the record that matches the criteria done field to true */
        public async Task<bool> MarkTaskAsDone(string taskUUID)
        {
            try
            {

                var result = await supabase.From<TaskItem>().Where(task => task.Id == taskUUID).Set(item => item.Done, true).Update();

                Console.WriteLine($"Result: {result}");

                if (result != null)
                {
                    if (result.ResponseMessage.IsSuccessStatusCode)
                    {
                        if (result.Models.Count > 0)
                        {
                            return true;
                        }

                        throw new NoRecordFoundException("Task was not found");

                    }

                    return false;
                }

                return false;
               
            } catch(Exception e)
            {
                throw;
            }
        }

        /** This method is in charge of deleting a task record
         The method queries the database and deletes the record that matches the criteria */
        public async Task<bool> DeleteTask(string taskUUID)
        {
            try
            {
                

                await supabase.From<TaskItem>().Where(task => task.Id == taskUUID).Delete();

                return true;

            } catch(Exception e)
            {
                Console.WriteLine($"Error deleting task: {e}");
                throw;
            }
        }
    }
}
