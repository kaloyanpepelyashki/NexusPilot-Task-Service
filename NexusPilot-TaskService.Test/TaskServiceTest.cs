using FluentAssertions;
using Moq;
using NexusPilot_Tasks_Service_src.Services;


namespace NexusPilot_TaskService.Test
{
    public class TaskServiceTest
    {
        private readonly Mock<TaskService> taskService;

        public TaskServiceTest() 
        { 
            taskService = new Mock<TaskService>();
        }


        [Fact]
        public void TaskService_GetInstance_ShouldReturnTaskServiceInstance()
        {
            //Arrange

            //Act

            //Assert
            taskService.Should().BeOfType<Mock<TaskService>>();
        }
    }
}
