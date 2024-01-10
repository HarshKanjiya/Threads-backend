using System.ComponentModel.DataAnnotations;

namespace AuthAPI.microservice.Constants
{
    public class Constants
    {
        public class Device
        {
            public required DeviceType Type { get; set; }
            [Key] public required string RefreshToken { get; set; }

            public DateTime CreateAt { get; set; } = DateTime.Now;
        }
    }

    public enum DeviceType
    {
        ANDROID,
        IOS,
        BROWSER,
        PC
    }

    public enum GenderType
    {
        MALE,
        FEMALE,
        NON_BINARY,
        NOT_SPECIFIED
    }

    public enum RoleType
    {
        USER,
        ADMIN
    }
}
