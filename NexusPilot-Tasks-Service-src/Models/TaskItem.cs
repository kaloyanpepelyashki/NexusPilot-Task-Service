using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


namespace NexusPilot_Tasks_Service_src.Models
{
    [Table("tasks")]
    public class TaskItem: BaseModel
    {
        [PrimaryKey("id")]
        public string Id { get; set; }

        [Column("summary")]
        public string Summary { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("imageurl")]
        public string ImageUrl { get; set; }

        [Column("priority")]
        public string Pirority { get; set; }

        [Column("startdate")]
        public DateOnly StartDate { get; set; }

        [Column("enddate")]
        public DateOnly EndDate { get; set; }

        [Column("done")]
        public bool Done { get; set; }

        [Column("project_id")]
        public Guid ProjectId { get; set; }

        [Column("task_owneruuid")]
        public Guid TaskOwnerId { get; set; }

    }
}
