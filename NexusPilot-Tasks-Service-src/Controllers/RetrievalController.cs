using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexusPilot_Tasks_Service_src.Services;

namespace NexusPilot_Tasks_Service_src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetrievalController : ControllerBase
    {
        private readonly TaskService _taskService;

        public RetrievalController()
        {
            _taskService = TaskService.GetInstance();
        }

        [HttpPost("allProjectTasks")]
        public async Task<ActionResult> GetAllProjectTasks([FromBody] string projectUUID)
        {
            try
            {
                var result = await _taskService.GetAllTasksForProject(projectUUID);

                if(result.isSuccess)
                {
                    if(result.tasksList.Count > 0)
                    {
                        return Ok(result.tasksList);
                    }

                    return StatusCode(404, "No resources were found");
                }

                return StatusCode(500, "Error retrieving tasks");

            } catch(Exception e)
            {
                Console.WriteLine($"Error retrieving tasks: {e}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("taskAssignees")]
        public async Task<ActionResult> GetTaskAssignees([FromBody] string taskUUID)
        {
            try
            {
                var result = await _taskService.GetTaskAssignees(taskUUID);
                if(result.isSuccess)
                {
                    if (result.assigneesList.Count > 0)
                    {
                        return Ok(result.assigneesList);
                    }

                    return StatusCode(404, "No records found");
                }

                return StatusCode(500, "Error getting records");

            } catch(Exception e)
            {
                Console.WriteLine($"Error getting task assignees: {e}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
