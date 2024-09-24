using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Domain.Common.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Catalog.Games;

public class RemovePlayerRequest : IRequest<Guid>
{
    public  DefaultIdType UserId { get; set; }
    public DefaultIdType GameId { get; set; }
}
public class RemovePlayerRequestHandler : IRequestHandler<RemovePlayerRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Player> _repository;
    private readonly IUserService _userService;
    private readonly ILogger<RemovePlayerRequest> _logger;
    private readonly ICurrentUser _currentUser;
    private readonly INotificationSender _notifications;
    private readonly IStringLocalizer<AddPlayerRequest> _localizer;
    private readonly IRepository<Game> _repositoryGame;
    public RemovePlayerRequestHandler(IStringLocalizer<AddPlayerRequest> stringLocalizer, IRepositoryWithEvents<Player> repository, IRepository<Game> repositoryGame, IUserService userService, ICurrentUser currentUser, ILogger<RemovePlayerRequest> logger, INotificationSender notificationSender)
    {
        _repository = repository;
        _userService = userService;
        _currentUser = currentUser;
        _logger = logger;
        _notifications = notificationSender;
        _localizer = stringLocalizer;
        _repositoryGame = repositoryGame;
    }
    
    public async Task<Guid> Handle(RemovePlayerRequest request, CancellationToken cancellationToken)
    {
        if (request.UserId ==Guid.Empty)
        {
            request.UserId = _currentUser.GetUserId();
        }
        var playerExcist = await _repository.GetBySpecAsync(new PlayersByUserIdAndGameIdSpec(request.UserId, request.GameId));
        if (playerExcist != null)
        {
            //playerExcist.DomainEvents.Add(EntityDeletedEvent.WithEntity(playerExcist));
            await _repository.DeleteAsync(playerExcist,cancellationToken);
            var gameinfo = await GetGameInfo(request.GameId, cancellationToken);
            if (gameinfo != null)
            {
                await SendNotificaion($"{_localizer["Player leave from "]} {gameinfo.Name}", gameinfo.CreatedBy.ToString(), cancellationToken);
            }
        }
        return playerExcist.Id;
    }
    private Task SendNotificaion(string message, string userid, CancellationToken cancellationToken)
    {

        var basicNot = new BasicNotification();
        basicNot.Message = message;
        basicNot.Label = BasicNotification.LabelType.Information;
        return _notifications.SendToUserAsync(basicNot, userid, cancellationToken);
    }
    private async Task<Game?> GetGameInfo(Guid gameId, CancellationToken cancellationToken)
    {
        var game = await _repositoryGame.GetByIdAsync(gameId, cancellationToken);
        return game;
    }
}
