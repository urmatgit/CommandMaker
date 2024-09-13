using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
    private readonly IUserService _userService;
    private readonly ILogger<AddPlayerRequest> _logger;
    private readonly ICurrentUser _currentUser;    
    public AddPlayerRequestHandler(IRepositoryWithEvents<Player> repository, IUserService userService,ICurrentUser currentUser, ILogger<AddPlayerRequest> logger) {
        _repository = repository;
        _userService = userService;
        _currentUser = currentUser;
        _logger = logger;
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

            await _repository.AddAsync(player, cancellationToken);

            return player.Id;
        }
        else
            return playerExcist.Id;
    }
}
