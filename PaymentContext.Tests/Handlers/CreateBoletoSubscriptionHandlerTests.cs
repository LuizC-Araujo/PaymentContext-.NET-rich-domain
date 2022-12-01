using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

[TestClass]
public class CreateBoletoSubscriptionHandlerTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var handler = new CreateBoletoSubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
        var command = new CreateBoletoSubscriptionCommand();
        
    command.FirstName = "Peter"; 
    command.LastName = "Parker"; 
    command.Document = "99999999999"; 
    command.Email = "peterparker@spider.com.br"; 
    command.BarCode = "652185418417"; 
    command.BoletoNumber = "541588478"; 
    command.PaymentNumber = "489259"; 
    command.PaidDate = DateTime.Now; 
    command.ExpireDate = DateTime.Now.AddMonths(1); 
    command.Total = 60; 
    command.TotalPaid = 60; 
    command.Payer = "Spider Corp"; 
    command.PayerDocument = "8888888888"; 
    command.PayerDocumentType = EDocumentType.CPF; 
    command.PayerEmail = "spider@spider.com.br"; 
    command.Street = "assas"; 
    command.Number = "588"; 
    command.Neighborhood = "asfds"; 
    command.City = "adasd"; 
    command.State = "sa"; 
    command.Country = "sa"; 
    command.ZipCode = "25454889";

    handler.Handle(command);
    Assert.AreEqual(false, handler.IsValid);
    }
}
