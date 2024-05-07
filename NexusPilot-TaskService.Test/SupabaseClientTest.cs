using FluentAssertions;
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
        [Fact]
        public void SupabaseClient_GetInstance_ShouldReturnSupabaseClientInstance()
        {
            //Arrange

            //Act
            var supabaseClient = SupabaseClient.GetInstance();

            //Assert
            supabaseClient.Should().NotBeNull();
            supabaseClient.Should().BeOfType<SupabaseClient>();
        }

        [Fact]
        public void SupabaseClient_TwoGetInstance_ShouldBeTheSame()
        {
            //Arrange

            //Act
            var supabaseClientInstance1 = SupabaseClient.GetInstance();
            var supabaseClientInstance2 = SupabaseClient.GetInstance();

            //Assert
            Assert.Same(supabaseClientInstance1, supabaseClientInstance2);
        }
    }
}
