using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace NexusPilot_Tasks_Service_src.Models
{
    [Table("taskassignees")]
    public class Assignee: BaseModel
    {
        [PrimaryKey("taskid", true)]
        public Guid TaskId { get; set; }

        [PrimaryKey("userid", true)]
        public Guid AssigneeId { get; set; }

        [Column("assignee_nickname")]
        public string AssigneeNickName { get; set; }
    }
}
