using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Domain.Common.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Catalog.Games;

public class AddPlayerRequest : IRequest<Guid>
{
    public  DefaultIdType UserId { get; set; }
    public DefaultIdType GameId { get; set; }
}

public class AddPlayerRequestHandler : IRequestHandler<AddPlayerRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Player> _repository;
    private readonly IRepository<Game> _repositoryGame;
    private readonly IUserService _userService;
    private readonly ILogger<AddPlayerRequest> _logger;
    private readonly ICurrentUser _currentUser;
    private readonly INotificationSender _notifications;
    private readonly IStringLocalizer<AddPlayerRequest> _localizer;
    public AddPlayerRequestHandler(IStringLocalizer<AddPlayerRequest> stringLocalizer, IRepositoryWithEvents<Player> repository, IRepository<Game> repositoryGame,  IUserService userService,ICurrentUser currentUser, ILogger<AddPlayerRequest> logger,INotificationSender notificationSender) {
        _repository = repository;
        _userService = userService;
        _currentUser = currentUser;
        _logger = logger;
        _notifications = notificationSender;
        _repositoryGame = repositoryGame;
        _localizer = stringLocalizer;
    }
    private Task SendNotificaion(string message,string userid,  CancellationToken cancellationToken)
    {
        
        var basicNot = new BasicNotification();
        basicNot.Message = message;
        basicNot.Label = BasicNotification.LabelType.Information;
        return _notifications.SendToUserAsync(basicNot, userid, cancellationToken);
    }
    private async Task<Game?> GetGameInfo(Guid gameId, CancellationToken cancellationToken) {
        var game = await _repositoryGame.GetByIdAsync(gameId, cancellationToken);
        return game;
    }
    public async Task<Guid> Handle(AddPlayerRequest request, CancellationToken cancellationToken)
    {
        if (request.UserId ==Guid.Empty)
        {
            request.UserId = _currentUser.GetUserId();
        }
        var playerExcist = await _repository.GetBySpecAsync(new PlayersByUserIdAndGameIdSpec(request.UserId, request.GameId),cancellationToken);
        if (playerExcist == null)
        {
            var userInfo = await _userService.GetAsync(request.UserId.ToString(), cancellationToken);

            var calcAge = userInfo.BirthDate.HasValue? DateTime.Now.Year - userInfo.BirthDate.Value.Year:25;
            var player = new Player(userInfo.UserName, userInfo.PhoneNumber, (byte)calcAge, 3, request.UserId, null, request.GameId);
           // player.DomainEvents.Add(EntityCreatedEvent.WithEntity(player));
            await _repository.AddAsync(player, cancellationToken);
            var gameinfo=await GetGameInfo(request.GameId, cancellationToken);
            if (gameinfo != null)
            {
                await SendNotificaion($"{_localizer["Added user to"]} {gameinfo.Name}", gameinfo.CreatedBy.ToString(), cancellationToken);
            }
            return player.Id;
        }
        else
            return playerExcist.Id;
    }
}
