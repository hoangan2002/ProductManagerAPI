using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Contract.Abstractions.Message;

namespace BE.Contract.Services.Product;
public static class DomainEvent
{
    public record ProductCreated(Guid Id) : IDomainEvent;
    public record ProductDeleted(Guid Id) : IDomainEvent;
    public record ProductUpdated(Guid Id) : IDomainEvent;
}
