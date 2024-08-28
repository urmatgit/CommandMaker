namespace FSH.WebApi.Application.Catalog.Games;

public class SearchGamesRequest : PaginationFilter, IRequest<PaginationResponse<GameDto>>
{    
}

public class GamesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Game, GameDto>
{
    public GamesBySearchRequestSpec(SearchGamesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchGamesRequestHandler : IRequestHandler<SearchGamesRequest, PaginationResponse<GameDto>>
{
    private readonly IReadRepository<Game> _repository;

    public SearchGamesRequestHandler(IReadRepository<Game> repository) => _repository = repository;

    public async Task<PaginationResponse<GameDto>> Handle(SearchGamesRequest request, CancellationToken cancellationToken)
    {
        var spec = new GamesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }

}