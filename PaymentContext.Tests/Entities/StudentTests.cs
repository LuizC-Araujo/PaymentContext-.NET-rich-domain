using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var subscription = new Subscription(null);
        var student = new Student("Luiz", "Araujo","12345678910", "luiz@luiz.com");
        student.AddSubscription(subscription);
    }
}