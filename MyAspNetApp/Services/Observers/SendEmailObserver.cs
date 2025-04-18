using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;
using MyAspNetApp.Utils.Template;


namespace MyAspNetApp.Services.Observers
{
    public class SendEmailObserver : IOrderObserver
    {
        private readonly IUserRepository _userRepository;
        private readonly Mailer _mailer;

        public SendEmailObserver(IUserRepository userRepository, Mailer mailer)
        {
            _userRepository = userRepository;
            _mailer = mailer;
        }

        public async Task OnOrderUpdatedAsync(Order order)
        {
            if (order.Status != "succeeded")
                return;

            var user = await _userRepository.FindUserById(order.UserId);

            if (user == null)
            {
                Console.WriteLine("[SendEmailObserver] User not found.");
                return;
            }

            var nameToShow = user.Name ?? user.Email;
            var orderCode = $"ORDER-{order.Id:D6}";
            var totalPrice = order.TotalPrice;
            var paidAt = DateTime.Now;

            string htmlContent = PaymentSuccessTemplate.Generate(
                nameToShow,
                orderCode,
                totalPrice,
                paidAt
            );

            await _mailer.SendMail(user.Email, "Payment Confirmation", htmlContent);

            // Console.WriteLine($"[SendEmailObserver] Sent payment confirmation to {user.Email} for Order {order.Id}");
        }
    }
}