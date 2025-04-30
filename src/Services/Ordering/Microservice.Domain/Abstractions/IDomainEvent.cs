using MediatR;

namespace Microservice.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid EventId=> Guid.NewGuid();
        public DateTime OccuredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName!;
    }
}