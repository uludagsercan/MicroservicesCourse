

using Microservice.Domain.Abstractions;
using Microservice.Domain.Models;

namespace Microservice.Domain.Events
{
    public record OrderCreatedDomainEvent(Order Order):IDomainEvent;
   
}