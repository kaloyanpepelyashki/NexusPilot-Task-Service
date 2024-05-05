﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexusPilot_Tasks_Service_src.Services;

namespace NexusPilot_Tasks_Service_src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeletionController : ControllerBase
    {
        private readonly TaskService _taskService;

        public DeletionController()
        {
            _taskService = TaskService.GetInstance();
        }

        [HttpDelete("taskByTaskId")]
        public ActionResult DeleteTaskById(string taskId)
        {
            try
            {
                var result = _taskService.DeleteTask(taskId);

                return Ok();
            } catch(Exception e)
            {
                return StatusCode(500, "Error deleting task: Internal Server error");
            }
        }

    }
}