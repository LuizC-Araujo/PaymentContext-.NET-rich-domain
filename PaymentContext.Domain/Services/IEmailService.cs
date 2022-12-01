using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Services;

public interface IEmailService
{
    void Send(string to, string email, string subject, string body);
}