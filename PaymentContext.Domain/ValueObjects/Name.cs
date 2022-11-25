using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(new Contract<Name>()
            .Requires().IsNullOrEmpty(FirstName, "Name.FirstName", "Nome não pode ser nulo ou vazio!")
            .Requires().IsNullOrEmpty(LastName, "Nome.LastName", "Sobrenome não pode ser nulo ou vazio!"));
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

}
