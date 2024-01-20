using System.ComponentModel.DataAnnotations;

namespace AuthAPI.microservice.Constants
{
    public class Constants
    {

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
        KEEP_SECRET
    }

    public enum RoleType
    {
        USER,
        ADMIN
    }
}
