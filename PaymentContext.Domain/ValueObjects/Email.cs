using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(
                new Contract()
                .Requires()
                .IsEmail(Address, "Email.Address", "This e-mail is invalid")
            );
    
        }

        public string Address { get; private set; }

    }
}