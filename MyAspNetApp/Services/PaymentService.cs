using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;
using MyAspNetApp.Utils.Template;
using MyAspNetApp.Data;

namespace MyAspNetApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly Mailer _mailer;
        private readonly ApplicationDbContext _context;
        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, Mailer mailer, IUserRepository userRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _mailer = mailer;
        }

        public async Task<Payment> CreatePayment(CreatePaymentDto createPaymentDto)
        {
            var existed = await _orderRepository.GetOrderById(createPaymentDto.OrderId);
            if(existed == null){
                throw new Exception("Order khong ton tai");
            }


            var payment = new Payment
            {
                OrderId = createPaymentDto.OrderId,
                PaymentProvider = createPaymentDto.PaymentProvider,
                PaymentIntentId = createPaymentDto.PaymentIntentId,
                TransactionId = createPaymentDto.TransactionId,
                Status = "Success",
                PaymentMethod = createPaymentDto.PaymentMethod,
                Amount = createPaymentDto.Amount,
                PaidAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            return await _paymentRepository.CreatePayment(payment);
        }

        
        // public async Task<string> Notification(int orderId)
        // {
        //     var order = await _orderRepository.GetOrderById(orderId);
        //     if (order == null)
        //     {
        //         Console.WriteLine($"[Notification] Order not found with OrderId = {orderId}");
        //         throw new Exception("Order not found.");
        //     }

        //     var user = await _userRepository.FindUserById(order.UserId);

        //     if (user == null)
        //         throw new Exception("User not found.");

        //     var nameToShow = user.Name ?? user.Email;
        //     var orderCode = $"ORDER-{order.Id:D6}";
        //     var totalPrice = order.TotalPrice;
        //     var paidAt = DateTime.Now;

        //     string htmlContent = PaymentSuccessTemplate.Generate(
        //         nameToShow,
        //         orderCode,
        //         totalPrice,
        //         paidAt
        //     );


        //     await _mailer.SendMail(user.Email, "Payment Confirmation", htmlContent);

        //     return "Notification sent successfully";
        // }
    }

}