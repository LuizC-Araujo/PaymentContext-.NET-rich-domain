using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities;

public class Student : Entity
{
    private IList<Subscription> _subscriptions;
    public Student(Name name, Document document, Email email)
    {
        Name = name;
        Document = document;
        Email = email;
        _subscriptions = new List<Subscription>();
        
        AddNotifications(name, document, email);
    }

    public Name Name { get; set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.ToArray();

    public void AddSubscription(Subscription subscription)
    {
        var hasSubscriptionActive = false;
        foreach (var sub in _subscriptions)
        {
            if (sub.Active)
                hasSubscriptionActive = true;
        }
        
        // AddNotifications(new Contract<Student>()
        //     .Requires()
        //     .IsFalse(hasSubscriptionActive,"Student.Subscription","Você já tem uma assinatura ativa")
        //     .AreEquals(0, subscription.Payments.Count, "Student.Subscription.Payments", "Esta assinatura não possui pagamentos"));

        // Alternativa
        if (hasSubscriptionActive)
        {
            AddNotification("Student.Subscription", "Voce já possui uma assinatura ativa!");
            return;
        }

        if (subscription.Payments.Count is 0)
        {
            AddNotification("Student.Subscription.Payments", "Essa assinatura não possui pagamentos");
            return;
        }
        
        _subscriptions.Add(subscription);
    }
}