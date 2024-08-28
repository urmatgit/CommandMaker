using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Teams.EventHandlers;

public class TeamDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<Team>>
{
    private readonly ILogger<TeamDeletedEventHandler> _logger;

    public TeamDeletedEventHandler(ILogger<TeamDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<Team> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}