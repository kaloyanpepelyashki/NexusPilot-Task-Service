namespace NexusPilot_Tasks_Service_src.Models.ExceptionModels
{

    //This exception is thrown, when no record of the requested recourse was found
    public class NoRecordFoundException: Exception
    {
        public NoRecordFoundException(string message): base(message) { }
    }
}
