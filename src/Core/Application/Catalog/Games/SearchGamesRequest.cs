using FSH.WebApi.Application.Catalog.Players;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Shared.Authorization;

namespace FSH.WebApi.Application.Catalog.Games;

public class SearchGamesRequest : PaginationFilter, IRequest<PaginationResponse<GameDto>>
{    
}

public class GamesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Game, GameDto>
{
    private readonly IUserService _userService;
    public GamesBySearchRequestSpec(SearchGamesRequest request, Guid onlyownData)
        : base(request)
    {

        Query.OrderBy(c => c.Name, !request.HasOrderBy());
        Query.Where(x => x.CreatedBy == onlyownData, onlyownData != Guid.Empty);


    }
}

public class SearchGamesRequestHandler : IRequestHandler<SearchGamesRequest, PaginationResponse<GameDto>>
{
    private readonly IReadRepository<Game> _repository;
    private readonly IReadRepository<Player> _repositoryPlyer;
    private IUserService _userService;
        private ICurrentUser _currentUser;
        public SearchGamesRequestHandler(IReadRepository<Game> repository, IUserService userService, ICurrentUser currentUser,IReadRepository<Player> repositoryPlayer )
        {
            _repository = repository;
        _repositoryPlyer = repositoryPlayer;    
            _userService = userService;

            _currentUser = currentUser;
        }

    public async Task<PaginationResponse<GameDto>> Handle(SearchGamesRequest request, CancellationToken cancellationToken)
    {
            var currentUserId = _currentUser.GetUserId();
            var allData = await _userService.HasPermissionAsync(currentUserId.ToString(), FSHPermission.NameFor(FSHAction.ViewAll, FSHResource.Games));
        //var roles = await _userService.GetRolesAsync(currentUserId.ToString(), cancellationToken);
       
            allData = allData || await _userService.IsInRoleAsync(currentUserId.ToString(), FSHRoles.Basic,cancellationToken); 
            var spec = new GamesBySearchRequestSpec(request,!allData?currentUserId: Guid.Empty);
            var games= await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
        if (games.TotalCount > 0) {
            var players = await _repositoryPlyer.GetListAsync(new PlayersByUserIdAndGameIdSpec(currentUserId, Guid.Empty), cancellationToken);
            if (players.Count() > 0)
            {
                games.Data.ForEach(player =>
                {
                    player.CurrentUserIn = players.Any(x => x.GameId == player.Id);
                });
            }
        }
        return games;
    }

}