using System.ComponentModel.DataAnnotations;

namespace Admin.microservice.Model
{
    public class EnvVarModel
    {
        [Key] public Guid VarId { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool SecretKey { get; set; } = false;
    }
}
