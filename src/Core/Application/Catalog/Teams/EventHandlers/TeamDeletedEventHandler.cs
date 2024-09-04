using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Teams.EventHandlers;

public class TeamDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<Team>>
{
    private readonly ILogger<TeamDeletedEventHandler> _logger;
    private readonly INotificationSender _notifications;
    private readonly ICurrentUser _currentUser;
    public TeamDeletedEventHandler(ILogger<TeamDeletedEventHandler> logger, INotificationSender notificationSender,ICurrentUser currentUser) => (_logger, _notifications, _currentUser) = (logger, notificationSender,currentUser);

    public override Task Handle(EntityDeletedEvent<Team> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        var name = @event.Entity.Name;
        var basicNot = new BasicNotification();
        basicNot.Message = $"{name} deleted";
        basicNot.Label = BasicNotification.LabelType.Information;
        return _notifications.SendToUserAsync(basicNot,_currentUser.GetUserId().ToString(), cancellationToken);
    }
}