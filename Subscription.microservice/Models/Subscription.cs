using System.ComponentModel.DataAnnotations;

namespace Subscription.microservice.Models
{
    public class SubscriptionModel
    {
        [Key] public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PackageName { get; set; }
        public string PurchasedPrice { get; set; }
        public string ReceiptId { get; set; }
        public string PurchaseStatus { get; set; } = "PENDING"; // PENDING, PURCHASED, CANCLED

    }
}
