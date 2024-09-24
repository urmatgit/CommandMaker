using FSH.WebApi.Domain.Common.Events;
using MediatR;

namespace FSH.WebApi.Application.Catalog.Teams.EventHandlers;

public class TeamCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<Team>>
{
    private readonly ILogger<TeamCreatedEventHandler> _logger;
    private readonly INotificationSender _notifications;
    public TeamCreatedEventHandler(ILogger<TeamCreatedEventHandler> logger,INotificationSender notificationSender) => (_logger,_notifications) = (logger,notificationSender);

    public override  Task Handle(EntityCreatedEvent<Team> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        var name = @event.GetType().Name;
        var basicNot = new BasicNotification();
        basicNot.Message = name;
        basicNot.Label = BasicNotification.LabelType.Information;
         return  _notifications.SendToUserAsync(basicNot,"Admin", cancellationToken);
        //return Task.CompletedTask;
    }
}