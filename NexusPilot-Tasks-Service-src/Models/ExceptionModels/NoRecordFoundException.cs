namespace NexusPilot_Tasks_Service_src.Models.ExceptionModels
{

    /*This exception is thrown, when no record of the requested recourse was found
     * This exception should be used, when the operation requested, requires at least one record to be found
     */
    public class NoRecordFoundException: Exception
    {
        public NoRecordFoundException(string message): base(message) { }
    }
}
