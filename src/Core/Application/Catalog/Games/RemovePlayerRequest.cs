using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Domain.Common.Events;
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
    public RemovePlayerRequestHandler(IRepositoryWithEvents<Player> repository, IUserService userService,ICurrentUser currentUser, ILogger<RemovePlayerRequest> logger) {
        _repository = repository;
        _userService = userService;
        _currentUser = currentUser;
        _logger = logger;
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
            await _repository.DeleteAsync(playerExcist,cancellationToken);
        }
        return playerExcist.Id;
    }
}
