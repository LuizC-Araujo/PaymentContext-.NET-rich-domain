using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class CreateBoletoSubscriptionHandler : SubscriptionHandler, IHandler<CreateBoletoSubscriptionCommand>
{
    public CreateBoletoSubscriptionHandler(IStudentRepository repository, IEmailService emailService) : base(repository, emailService)
    {
    }
    
    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        // Fail Fast Validation
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar sua assinatura"); 
        }
            
        // Verificar se Documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está incluso");
        
        // Verificar se E-mail já está cadastrado
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já está incluso");
        
        // Gerar os VOs
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State,
            command.Country, command.ZipCode);

        // Gerar as Entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, 
            command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), address, email, 
            command.BarCode, command.BoletoNumber);
        
        // Relacionamentos 
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);
        
        // Agrupar as Validações
        AddNotifications(name, document, address, student, subscription, payment);
        
        // Salvar as infomrções
        _repository.CreateSubscription(student);
        
        // Enviar e-mail de boas vindas
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi criada");
        
        // Retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso");
    }
}