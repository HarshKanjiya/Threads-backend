namespace Subscription.microservice.Model.DTOs
{
    public class StripeSecretKeyResponseDTO
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public StripeSecretKeyDTO? Data { get; set; }
    }
    public class StripeSecretKeyDTO
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
