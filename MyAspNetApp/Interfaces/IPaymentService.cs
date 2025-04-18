using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> CreatePayment(CreatePaymentDto createPaymentDto);
        // Task<string> Notification(int orderId);
    }
}