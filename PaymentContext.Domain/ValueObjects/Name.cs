using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(
                new Contract().
                Requires()
                .HasMinLen(FirstName,3,"Name.FirstName","The name must contain at least 3 characters.")
                .HasMaxLen(FirstName,40,"Name.FirstName","The name must contain a maximum of 40 characters.")
                .HasMinLen(LastName,3,"Name.LastName","The lastname must contain at least 3 characters.")
                .HasMaxLen(LastName,40,"Name.LastName","The lastname must contain a maximum of 40 characters.")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

    }
}