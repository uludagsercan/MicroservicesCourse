

using Microservice.Domain.Abstractions;
using Microservice.Domain.Models;

namespace Microservice.Domain.Events
{
    public record OrderUpdatedDomainEvent(Order Order):IDomainEvent;
 
}