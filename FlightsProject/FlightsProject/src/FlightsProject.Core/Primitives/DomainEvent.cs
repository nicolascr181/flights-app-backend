using MediatR;

namespace FlightsProject.Core.Primitives;
public record DomainEvent(Guid Id) : INotification;
