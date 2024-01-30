namespace UserActions.microservice.Models.DTOs
{
    public class getThreadInfoDTO
    {
        public Guid ThreadId { get; set; }
        public required string AuthorId { get; set; }
        public required string Type { get; set; } = "PARENT";
        public string? ReferenceId { get; set; }
        public required string ReplyAccess { get; set; } = "ANY";
    }
    public class getThreadInfoResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public getThreadInfoDTO? Data { get; set; }
    }
    public class getUserInfoDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string AvatarURL { get; set; }
    }
    public class getUserInfoResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public getUserInfoDTO? Data { get; set; }
    }

}
