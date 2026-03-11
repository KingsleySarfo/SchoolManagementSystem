namespace SchoolManagement.API.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public ApiResponse() { }

        public ApiResponse(T data, string message = "")
        {
            Data = data;
            Message = message;
        }

        public ApiResponse(string message, bool success = false)
        {
            Success = success;
            Message = message;
        }
    }
}