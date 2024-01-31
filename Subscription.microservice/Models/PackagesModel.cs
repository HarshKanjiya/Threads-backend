using System.ComponentModel.DataAnnotations;

namespace Subscription.microservice.Models
{
    public class PackagesModel
    {
        [Key] public Guid PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackagePrice { get; set; }
        public string Discount { get; set; }
        public bool Active { get; set; } = false;
        public string AccentColor { get; set; }
        public List<string> Perks { get; set; } = new List<string>();
    }
}
