using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class PaymentTests
{
    private readonly Email _email;
    private readonly Document _document;
    private readonly Address _address;

    public PaymentTests()
    {
        _email = new Email("teste@teste.com.br");
        _document = new Document("35111507795", EDocumentType.CPF);
        _address = new Address("Rua 1", "1234", "Bairro Legal", "Gotham", "SP", "BR", "13400000");
    }

    [TestMethod]
    public void ShouldReturnErrorWhenTotalLessThanZero()
    {
        var _payment = new BoletoPayment(DateTime.Now, DateTime.Now.AddDays(10), -5, -5, "eu mesmo", _document,
            _address, _email, "adsdoapdokapokda", "5155454");
        
        Assert.IsTrue(!_payment.IsValid);
    }
    
    [TestMethod]
    public void ShouldReturnSucessWhenTotalEqualTotoalPaid()
    {
        var _payment = new BoletoPayment(DateTime.Now, DateTime.Now.AddDays(10), 20, 20, "eu mesmo", _document,
            _address, _email, "adsdoapdokapokda", "5155454");
        
        Assert.IsTrue(_payment.IsValid);
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenTotalLessThanTotalPaid()
    {
        var _payment = new BoletoPayment(DateTime.Now, DateTime.Now.AddDays(10), 15, 20, "eu mesmo", _document,
            _address, _email, "adsdoapdokapokda", "5155454");
        
        Assert.IsTrue(!_payment.IsValid);
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenTotalMoreThanTotalPaid()
    {
        var _payment = new BoletoPayment(DateTime.Now, DateTime.Now.AddDays(10), 25, 20, "eu mesmo", _document,
            _address, _email, "adsdoapdokapokda", "5155454");
        
        Assert.IsTrue(!_payment.IsValid);
    }
    
}