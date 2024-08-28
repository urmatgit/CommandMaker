namespace FSH.WebApi.Application.Catalog.Players;

public class GetPlayerRequest : IRequest<PlayerDetailsDto>
{
    public Guid Id { get; set; }

    public GetPlayerRequest(Guid id) => Id = id;
}

public class GetPlayerRequestHandler : IRequestHandler<GetPlayerRequest, PlayerDetailsDto>
{
    private readonly IRepository<Player> _repository;
    private readonly IStringLocalizer _t;

    public GetPlayerRequestHandler(IRepository<Player> repository, IStringLocalizer<GetPlayerRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);
     public async Task<PlayerDetailsDto> Handle(GetPlayerRequest request, CancellationToken cancellationToken) =>
 await _repository.GetBySpecAsync(
 (ISpecification<Player, PlayerDetailsDto>)new PlayerByIdWithTeamSpec(request.Id), cancellationToken)
 ?? throw new NotFoundException(_t["Player {0} Not Found.", request.Id]);


  
}