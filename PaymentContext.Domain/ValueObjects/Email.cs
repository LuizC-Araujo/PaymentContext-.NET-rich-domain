using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string address)
    {
        Address = address;
        
        AddNotifications(new Contract<Email>()
            .IsEmail(address, "Email.address", "Emai não é válido!"));
    }

    public string Address { get; private set; }
    
    
}