using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Teams.EventHandlers;

public class TeamUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<Team>>
{
    private readonly ILogger<TeamUpdatedEventHandler> _logger;

    public TeamUpdatedEventHandler(ILogger<TeamUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<Team> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}