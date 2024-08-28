using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Players.EventHandlers;

public class PlayerUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<Player>>
{
    private readonly ILogger<PlayerUpdatedEventHandler> _logger;

    public PlayerUpdatedEventHandler(ILogger<PlayerUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<Player> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}