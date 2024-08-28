namespace FSH.WebApi.Application.Catalog.Teams;

public class SearchTeamsRequest : PaginationFilter, IRequest<PaginationResponse<TeamDto>>
{    
    public Guid? GameId { get; set; }
	
}

public class SearchTeamsRequestHandler : IRequestHandler<SearchTeamsRequest, PaginationResponse<TeamDto>>
{
    private readonly IReadRepository<Team> _repository;

    public SearchTeamsRequestHandler(IReadRepository<Team> repository) => _repository = repository;

    public async Task<PaginationResponse<TeamDto>> Handle(SearchTeamsRequest request, CancellationToken cancellationToken)
    {
        var spec = new TeamsBySearchRequestWithGamesSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}