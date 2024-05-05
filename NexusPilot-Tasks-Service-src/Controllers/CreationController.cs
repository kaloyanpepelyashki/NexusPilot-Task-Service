using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexusPilot_Tasks_Service_src.Models;
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

        [HttpPost("newTask")]
        public async Task<ActionResult> CreateTask([FromBody] TaskCreationObject taskObject)
        {
            try
            {

                var result = await _taskService.CreateNewTask(taskObject.TaskOwnerUUID, taskObject.ProjectUUID, taskObject.Summary, taskObject.Description, taskObject.ImageUrl, taskObject.Priority);

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
            public string Priority { get; set; }

        }

    }
}
