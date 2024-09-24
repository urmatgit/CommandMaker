using FSH.WebApi.Domain.Common.Events;
using FSH.WebApi.Domain.Identity;
using FSH.WebApi.Shared.Events;

namespace FSH.WebApi.Application.Dashboard;

public class SendStatsChangedNotificationHandler :
    IEventNotificationHandler<EntityCreatedEvent<Player>>,
    IEventNotificationHandler<EntityDeletedEvent<Player>>,
    IEventNotificationHandler<EntityCreatedEvent<Team>>,
    IEventNotificationHandler<EntityDeletedEvent<Team>>,
    IEventNotificationHandler<EntityCreatedEvent<Game>>,
    IEventNotificationHandler<EntityDeletedEvent<Game>>,
    IEventNotificationHandler<ApplicationRoleCreatedEvent>,
    IEventNotificationHandler<ApplicationRoleDeletedEvent>,
    IEventNotificationHandler<ApplicationUserCreatedEvent>
{
    private readonly ILogger<SendStatsChangedNotificationHandler> _logger;
    private readonly INotificationSender _notifications;

    public SendStatsChangedNotificationHandler(ILogger<SendStatsChangedNotificationHandler> logger, INotificationSender notifications) =>
        (_logger, _notifications) = (logger, notifications);

    public Task Handle(EventNotification<EntityCreatedEvent<Player>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<Player>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    public Task Handle(EventNotification<EntityCreatedEvent<Team>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<Team>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityCreatedEvent<Game>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<EntityDeletedEvent<Game>> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<ApplicationRoleCreatedEvent> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<ApplicationRoleDeletedEvent> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);
    public Task Handle(EventNotification<ApplicationUserCreatedEvent> notification, CancellationToken cancellationToken) =>
        SendStatsChangedNotification(notification.Event, cancellationToken);

    private Task SendStatsChangedNotification(IEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered => Sending StatsChangedNotification", @event.GetType().Name);

        return _notifications.SendToAllAsync(new StatsChangedNotification(), cancellationToken);
    }
}