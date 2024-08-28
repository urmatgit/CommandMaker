namespace FSH.WebApi.Application.Catalog.Games;

public class GetGameRequest : IRequest<GameDto>
{
    public Guid Id { get; set; }

    public GetGameRequest(Guid id) => Id = id;
}

public class GameByIdSpec : Specification<Game, GameDto>, ISingleResultSpecification
{
    public GameByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetGameRequestHandler : IRequestHandler<GetGameRequest, GameDto>
{
    private readonly IRepository<Game> _repository;
    private readonly IStringLocalizer<GetGameRequestHandler> _t;

    public GetGameRequestHandler(IRepository<Game> repository, IStringLocalizer<GetGameRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<GameDto> Handle(GetGameRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
        (ISpecification<Game, GameDto>)new GameByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["game {0} Not Found", request.Id]);
}