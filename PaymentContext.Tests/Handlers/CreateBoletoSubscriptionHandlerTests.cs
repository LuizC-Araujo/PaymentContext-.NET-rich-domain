using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

[TestClass]
public class CreateBoletoSubscriptionHandlerTests
{
    private readonly CreateBoletoSubscriptionCommand _command;
    private readonly CreateBoletoSubscriptionHandler _handler;

    public CreateBoletoSubscriptionHandlerTests()
    {
        _handler = new CreateBoletoSubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
        _command = new CreateBoletoSubscriptionCommand();
        
        _command.FirstName = "Peter";
        _command.LastName = "Parker";
        _command.Document = "00000000000";
        _command.Email = "peterparker@spider.com.br";
        _command.BarCode = "652185418417";
        _command.BoletoNumber = "541588478";
        _command.PaymentNumber = "489259";
        _command.PaidDate = DateTime.Now;
        _command.ExpireDate = DateTime.Now.AddMonths(1);
        _command.Total = 60;
        _command.TotalPaid = 60;
        _command.Payer = "Spider Corp";
        _command.PayerDocument = "8888888888";
        _command.PayerDocumentType = EDocumentType.CPF;
        _command.PayerEmail = "spider@spider.com.br";
        _command.Street = "assas";
        _command.Number = "588";
        _command.Neighborhood = "asfds";
        _command.City = "adasd";
        _command.State = "sa";
        _command.Country = "sa";
        _command.ZipCode = "25454889";
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    { 
        _command.Document = "99999999999";
        
        _handler.Handle(_command);
        Assert.AreEqual(false, _handler.IsValid);
    }
}
