namespace NexusPilot_Tasks_Service_src.Models.ExceptionModels
{
    public class AlreadyExistsException: Exception
    {
        public AlreadyExistsException(string message): base(message) { }
    }
}
