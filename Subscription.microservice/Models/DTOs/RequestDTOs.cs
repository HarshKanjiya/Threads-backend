using System.Xml;

namespace Subscription.microservice.Models.DTOs
{
    public class PaymentIntentRequestDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PackageName { get; set; }
        public int Ammount { get; set; }
    }

    public class CreateUpdatePackageRequestDTO
    {
        public string? PackageName { get; set; }
        public string? PackagePrice { get; set; }
        public string? Discount { get; set; }
        public bool Active { get; set; }
        public bool Published { get; set; }
        public List<string>? Perks { get; set; } = new List<string>();
        public string? AccentColor { get; set; }

    }
}
