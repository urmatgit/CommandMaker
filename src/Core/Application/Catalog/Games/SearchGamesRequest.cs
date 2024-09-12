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
        private IUserService _userService;
        private ICurrentUser _currentUser;
        public SearchGamesRequestHandler(IReadRepository<Game> repository, IUserService userService, ICurrentUser currentUser)
        {
            _repository = repository;
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
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }

}