using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Shared.Authorization;

namespace FSH.WebApi.Application.Catalog.Teams;

public class SearchTeamsRequest : PaginationFilter, IRequest<PaginationResponse<TeamDto>>
{    
    public Guid? GameId { get; set; }
	
}

public class SearchTeamsRequestHandler : IRequestHandler<SearchTeamsRequest, PaginationResponse<TeamDto>>
{
    private readonly IReadRepository<Team> _repository;
    private IUserService _userService;
    private ICurrentUser _currentUser;
    public SearchTeamsRequestHandler(IReadRepository<Team> repository,IUserService userService,ICurrentUser currentUser) { 
        _repository = repository;
        _userService = userService;
        _currentUser = currentUser;
}

    public async Task<PaginationResponse<TeamDto>> Handle(SearchTeamsRequest request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var allData = await _userService.HasPermissionAsync(currentUserId.ToString(), FSHPermission.NameFor(FSHAction.ViewAll, FSHResource.Games));
        allData = allData || await _userService.IsInRoleAsync(currentUserId.ToString(), FSHRoles.Basic, cancellationToken);
        var spec = new TeamsBySearchRequestWithGamesSpec(request,allData? Guid.Empty:currentUserId);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}