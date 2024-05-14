using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexusPilot_Tasks_Service_src.Models;
using NexusPilot_Tasks_Service_src.Models.ExceptionModels;
using NexusPilot_Tasks_Service_src.Services;
using System.ComponentModel.DataAnnotations;

namespace NexusPilot_Tasks_Service_src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreationController : ControllerBase
    {
        private readonly TaskService _taskService;

        public CreationController()
        {
            _taskService = TaskService.GetInstance();
        }

        [Authorize]
        [HttpPost("newTask")]
        public async Task<ActionResult> CreateTask([FromBody] TaskCreationObject taskObject)
        {
            try
            {

                var result = await _taskService.CreateNewTask(taskObject.TaskOwnerUUID, taskObject.ProjectUUID, taskObject.Summary, taskObject.Description, taskObject.ImageUrl, taskObject.Priority, taskObject.EndDate, taskObject.StartDate);

                if(result)
                {
                    return Ok("Task created successfully");
                }

                return StatusCode(500, "Error creating task. Internal Server Error");

            } catch(Exception e)
            {
                return StatusCode(500, "Error creating task. Internal Server Error");
            }
        }

        [Authorize]
        [HttpPost("addAssignee")]
        public async Task<ActionResult> AddAssigneesToTask([FromBody] AddAssigneeObject addAssigneeObject)
        {
            try
            {
                var result = await _taskService.AddAssigneeToTask(addAssigneeObject.TaskUUID, addAssigneeObject.UserUUID, addAssigneeObject.UserNickName);

                if(result)
                {
                    return Ok("Assignee added to task");
                }

                return StatusCode(500, "Uncertain Database transaction result");

            } catch(AlreadyExistsException e)
            {
                return StatusCode(400, "User already assigned to a task");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding assignee: {e}");
                return StatusCode(500, "Error adding assignee: Internal Server Error");
            }
        }


        public class TaskCreationObject 
        {
            [Required]
            public string TaskOwnerUUID { get; set; }

            [Required]
            public string ProjectUUID { get; set; }

            [Required]
            public string Summary { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            public string ImageUrl { get; set; }

            [Required]
            public DateOnly StartDate { get; set; }

            [Required]
            public DateOnly EndDate { get; set; }

            [Required]
            public string Priority { get; set; }

        }

        public class AddAssigneeObject
        {
            [Required]
            public string TaskUUID { get; set; }
            [Required]
            public string UserUUID { get; set; }
            [Required]
            public string UserNickName { get; set; }
        }

    }
}
