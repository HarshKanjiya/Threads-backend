﻿
namespace Subscription.microservice.Models.DTOs
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Object? Data { get; set; }

    }

    public class PaymentIntentDTO
    {
        public string Key { get; set; }
    }
}
