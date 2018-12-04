namespace Tupy
{
    public class ExecutionResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string Source { get; set; }
        public T Content { get; set; }

        public ExecutionResponse() : this(true)
        {

        }

        public ExecutionResponse(bool success) : this(success, null)
        {

        }

        public ExecutionResponse(bool success, string message)
        {
            IsSuccess = success;
            Message = message;
        }
    }
}