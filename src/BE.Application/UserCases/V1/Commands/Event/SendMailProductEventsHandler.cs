using BE.Contract.Abstractions.Message;
using BE.Contract.Services.Product;

namespace BE.Application.UserCases.V1.Commands.Event;
public sealed class SendMailProductEventsHandler : IDomainEventHandler<DomainEvent.ProductCreated>
                                                 , IDomainEventHandler<DomainEvent.ProductUpdated>
{
    public Task Handle(DomainEvent.ProductCreated notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Handle(DomainEvent.ProductUpdated notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
   

