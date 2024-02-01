using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;
using Subscription.microservice.data;
using Subscription.microservice.Model.DTOs;
using Subscription.microservice.Models;
using Subscription.microservice.Models.DTOs;
using System.Net.Http;

namespace Subscription.microservice.Controllers
{
    [Route("api/v1/buy")]
    [ApiController, Authorize]
    public class PurchaseSubscriptionController : ControllerBase
    {
        private readonly DBcontext db;
        private readonly HttpClient httpClient;
        public PurchaseSubscriptionController(DBcontext _db, HttpClient _httpClient)
        {
            db = _db;
            httpClient = _httpClient;
        }


        [HttpPost("init")]
        public async Task<ActionResult<ResponseDTO>> PurchaseMembership(PaymentIntentRequestDTO req)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                StripeSecretKeyResponseDTO secretKeyRes = await httpClient.GetFromJsonAsync<StripeSecretKeyResponseDTO>("https://localhost:7200/api/v1/env/STRIPE_SECRET");

                if (secretKeyRes?.Success == false)
                {
                    response.Message = "Payment initialization Failed [KEY]"; // remove [key] after dev
                    response.Success = false;
                    return Ok(response);
                }
                else
                {
                    StripeConfiguration.ApiKey = secretKeyRes?.Data?.Value;

                    var paymentIntentService = new PaymentIntentService();
                    var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
                    {
                        Amount = (int)req.Ammount * 100,
                        Currency = "inr",
                        ReceiptEmail = "",
                        AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                        {
                            Enabled = true,
                        },
                    });



                    SubscriptionModel data = new SubscriptionModel()
                    {
                        ReceiptId = paymentIntent.ClientSecret,
                        UserId = req.UserId,
                        UserName = req.UserName,
                        PackageName = req.PackageName,
                        PurchasedPrice = req.Ammount,
                        PurchaseStatus = "PENDING",

                    };
                    var dbSubscription = await db.Subscriptions.AddAsync(data);
                    await db.SaveChangesAsync();

                    if (dbSubscription.Entity != null)
                    {
                        PaymentIntentDTO ResObj = new PaymentIntentDTO()
                        {
                            Key = paymentIntent.ClientSecret
                        };

                        response.Success = true;
                        response.Data = ResObj;
                        return Ok(response);
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Payment Initialization Failed!";
                        return Ok(response);
                    }

                }
            }
            catch (Exception e)
            {
                response.Message = "Internal server error : Subscription :" + e.Message;
                response.Success = false;
                return BadRequest(response);
            }
        }

    }
}
