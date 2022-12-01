using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks;

public class FakeStudentRepository : IStudentRepository
{
    public bool DocumentExists(string document)
    {
        return document is "99999999999";
    }

    public bool EmailExists(string email)
    {
        return email is "luiz@luiz.com.br";
    }

    public void CreateSubscription(Student student)
    {
    }
}