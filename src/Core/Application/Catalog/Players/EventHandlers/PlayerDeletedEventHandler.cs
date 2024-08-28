using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Players.EventHandlers;

public class PlayerDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<Player>>
{
    private readonly ILogger<PlayerDeletedEventHandler> _logger;

    public PlayerDeletedEventHandler(ILogger<PlayerDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<Player> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}