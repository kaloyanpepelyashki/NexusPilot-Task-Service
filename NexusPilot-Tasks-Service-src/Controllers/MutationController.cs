﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexusPilot_Tasks_Service_src.Models.ExceptionModels;
using NexusPilot_Tasks_Service_src.Services;

namespace NexusPilot_Tasks_Service_src.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MutationController : ControllerBase
    {
        private readonly TaskService _taskService;

        public MutationController(TaskService taskService)
        {
            _taskService = taskService;
        }

        /*This method marks a task as done based on taskUUID, the method expects to receive string taskUUID */
        [Authorize]
        [HttpPatch("markTaskDone")]
        public async Task<ActionResult> MarkTaskDone([FromBody] string taskId)
        {
            try
            {
                var result = await _taskService.MarkTaskAsDone(taskId);

                if (result)
                {
                    return Ok("Task marked as done");
                }

                return StatusCode(500);

            } catch(NoRecordFoundException e)
            {
                return StatusCode(404, $"Error marking task as done: {e.Message}");
            }
            catch(Exception e)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
