using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Interfaces;
using Stripe;
using MyAspNetApp.Utils;
using System.Threading.Tasks;
using MyAspNetApp.Strategies;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        private readonly Logger _logger;
        private readonly IConfiguration _configuration;


        public PaymentController(IPaymentService paymentService, IOrderService orderService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _orderService = orderService;
            _logger = Logger.GetInstance();
            _configuration = configuration;
        }


        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentDto dto)
        {
            IPaymentStrategy strategy;
            if (dto.Method == "stripe")
                strategy = new StripePaymentStrategy();
            else if (dto.Method == "momo")
            {
                strategy = new MomoPaymentStrategy(_configuration);
                decimal exchangeRate = 25000m;
                dto.Amount *= exchangeRate;
            }
            else
                return BadRequest("Unsupported payment method.");

            var paymentContext = new PaymentContext();
            paymentContext.SetStrategy(strategy);
            
            var result = await paymentContext.ExecuteStrategy(dto.Amount, dto.OrderId);

            return Ok( result );
        }


        [HttpPost("confirm-payment")]
        public async Task<ActionResult> ConfirmPayment([FromBody] CreatePaymentDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.PaymentIntentId))
                {
                    return BadRequest(new { statusCode = 400, msg = "PaymentIntentId is required" });
                }

                if (dto.PaymentProvider == "Stripe")
                {
                    var service = StripeClientSingleton.Instance.PaymentIntentService; 
                    Console.WriteLine($"PaymentIntentService instance: {service.GetHashCode()}");
                        
                    var intent = await service.GetAsync(dto.PaymentIntentId);

                    if (intent == null)
                    {
                        return BadRequest(new { statusCode = 400, msg = "PaymentIntent not found" });
                    }

                    Console.WriteLine("PaymentIntentId: " + dto.PaymentIntentId);
                    Console.WriteLine("Trạng thái PaymentIntent: " + intent.Status);
                    Console.WriteLine("OrderId: " + dto.OrderId);

                    if (intent.Status == "succeeded")
                    {
                        var paymentDto = new CreatePaymentDto
                        {
                            OrderId = dto.OrderId,
                            PaymentProvider = "Stripe",
                            PaymentIntentId = intent.Id,
                            TransactionId = intent.Id,
                            PaymentMethod = intent.PaymentMethodTypes?.FirstOrDefault() ?? "unknown",
                            Amount = (decimal)intent.AmountReceived / 100.0m // Stripe returns long?
                        };

                        var payment = await _paymentService.CreatePayment(paymentDto);

                        var updateOrder = new UpdateOrderDto
                        {
                            OrderId = payment.OrderId,
                            Status = "succeeded"
                        };

                        await _orderService.UpdateOrder(updateOrder);

                        return Ok(new
                        {
                            statusCode = 200,
                            msg = "Payment confirmed successfully",
                            metadata = new
                            {
                                status = intent.Status
                            }
                        });
                    }
                }
                else if (dto.PaymentProvider == "Momo")
                {
                    if (string.IsNullOrEmpty(dto.TransactionId))
                    {
                        return BadRequest(new { statusCode = 400, msg = "TransactionId is required for Momo" });
                    }


                    var paymentDto = new CreatePaymentDto
                    {
                        OrderId = dto.OrderId,
                        PaymentProvider = "Momo",
                        PaymentIntentId = dto.TransactionId, // Momo không có PaymentIntentId, sử dụng TransactionId
                        TransactionId = dto.TransactionId,
                        PaymentMethod = "Wallet", // Hoặc phương thức thanh toán của Momo (tuỳ thuộc vào cách Momo cung cấp thông tin)
                        Amount = dto.Amount
                    };

                    var payment = await _paymentService.CreatePayment(paymentDto);

                    var updateOrder = new UpdateOrderDto
                    {
                        OrderId = payment.OrderId,
                        Status = "succeeded"
                    };

                    await _orderService.UpdateOrder(updateOrder);

                    return Ok(new
                    {
                        statusCode = 200,
                        msg = "Payment confirmed successfully",
                        metadata = new
                        {
                            status = "succeeded"
                        }
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        statusCode = 400,
                        msg = $"Momo payment not successful. Current status: "
                    });
                }
                
                return BadRequest(new { statusCode = 400, msg = "Payment not successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = ex.Message
                });
            }
        }


        

    }
}
