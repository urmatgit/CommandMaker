using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Teams.EventHandlers;

public class TeamCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Team>>
{
    private readonly ILogger<TeamCreatedEventHandler> _logger;

    public TeamCreatedEventHandler(ILogger<TeamCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<Team> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}