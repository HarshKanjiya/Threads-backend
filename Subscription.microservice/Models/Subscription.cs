using System.ComponentModel.DataAnnotations;
using static Subscription.microservice.Constants.Constants;

namespace Subscription.microservice.Models
{
    public class SubscriptionModel
    {
        [Key] public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public SubscriptionStatusType OrderStatus { get; set; } = SubscriptionStatusType.PENDING;

    }
}
