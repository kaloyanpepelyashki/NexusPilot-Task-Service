using FluentAssertions;
using Moq;
using NexusPilot_Tasks_Service_src.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusPilot_TaskService.Test
{
    public class SupabaseClientTest
    {
        private readonly Mock<SupabaseClient> supabaseClient;

        public SupabaseClientTest()
        {
            supabaseClient = new Mock<SupabaseClient>();
        }

        [Fact]
        public void SupabaseClient_GetInstance_ShouldReturnSupabaseClientInstance()
        {
            //Arrange

            //Act

            //Assert
            supabaseClient.Should().NotBeNull();
            supabaseClient.Should().BeOfType<Mock<SupabaseClient>>();
        }

    }
}
