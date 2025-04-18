using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces{

    public interface IPaymentRepository{
        Task<Payment> CreatePayment(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentById(int PaymentId);
    }

}