using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    public interface IStudentRepository
    {
        bool DocumentExistis(string document);
        bool EmailExistis(string document);

        void CreateSubscription(Student student);
    }
}