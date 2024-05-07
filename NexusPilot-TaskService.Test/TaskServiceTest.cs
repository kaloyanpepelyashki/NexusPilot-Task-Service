using FluentAssertions;
using NexusPilot_Tasks_Service_src.Services;


namespace NexusPilot_TaskService.Test
{
    public class TaskServiceTest
    {
        [Fact]
        public void TaskService_GetInstance_ShouldReturnTaskServiceInstance()
        {
            //Arrange

            //Act
            var taskService = TaskService.GetInstance();

            //Assert
            taskService.Should().BeOfType<TaskService>();
        }
    }
}
