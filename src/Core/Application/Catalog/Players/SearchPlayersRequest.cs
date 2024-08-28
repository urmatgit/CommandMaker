namespace FSH.WebApi.Application.Catalog.Players;

public class SearchPlayersRequest : PaginationFilter, IRequest<PaginationResponse<PlayerDto>>
{    
    public Guid? TeamId { get; set; }
	
}

public class SearchPlayersRequestHandler : IRequestHandler<SearchPlayersRequest, PaginationResponse<PlayerDto>>
{
    private readonly IReadRepository<Player> _repository;

    public SearchPlayersRequestHandler(IReadRepository<Player> repository) => _repository = repository;

    public async Task<PaginationResponse<PlayerDto>> Handle(SearchPlayersRequest request, CancellationToken cancellationToken)
    {
        var spec = new PlayersBySearchRequestWithTeamsSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}