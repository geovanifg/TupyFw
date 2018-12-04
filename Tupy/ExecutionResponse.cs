namespace Tupy
{
    public class ExecutionResponse : ExecutionResponse<string>
    {
        public ExecutionResponse() : base(true)
        {

        }

        public ExecutionResponse(bool success) : base(success, null)
        {

        }

        public ExecutionResponse(bool success, string message)
        {
            IsSuccess = success;
            Message = message;
        }
    }
}
