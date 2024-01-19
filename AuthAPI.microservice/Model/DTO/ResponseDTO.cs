namespace AuthAPI.microservice.Model.DTO
{
    public class ResponseDTO
    {
        public Object? Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
