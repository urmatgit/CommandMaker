using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Players.EventHandlers;

public class PlayerCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Player>>
{
    private readonly ILogger<PlayerCreatedEventHandler> _logger;

    public PlayerCreatedEventHandler(ILogger<PlayerCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<Player> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}