using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        public SubscriptionHandler(IStudentRepository repository)
        {
            _repository = repository;
        }
        public IcommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if(command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false,"Unable to register.");
            }

            if(_repository.DocumentExists(command.Document))
                AddNotification("Document","This document is already in use.");
                
            if(_repository.EmailExistis(command.Email))
                AddNotification("Email","This e-mail is already in use.");

            
             var name = new Name(command.FirstName,command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            var student = new Student(name,document,email);
            var subscription = new Subscription(DateTime.Now.AddDays(30));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, 
                command.PayerDocumentType),
                address,email);

                subscription.AddPayment(payment);
                student.AddNotifications(subscription);

                AddNotifications(name,document,email,address,student,subscription,payment);
                
                if(Invalid)
                    return new CommandResult(false,"Unable to register.");

                _repository.CreateSubscription(student);
            
            return new CommandResult(true, "Subscription completed successfully.");
        }
    }
}