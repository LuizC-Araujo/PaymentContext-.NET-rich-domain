using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(string number, EDocumentType type)
    {
        Number = number;
        Type = type;
        
        AddNotifications(new Contract<Document>()
            .Requires()
            .IsTrue(Validate(), "Document.Number", "Documento inválido"));
    }
    public string Number { get; private set; }
    public EDocumentType Type { get; private set; }

    private bool Validate()
    {
        return Type switch
        {
            EDocumentType.CNPJ when Number.Length is 14 => true,
            EDocumentType.CPF when Number.Length is 11 => true,
            _ => false
        };
    }
}