namespace FSH.WebApi.Application.Catalog.Teams;

public class GetTeamRequest : IRequest<TeamDetailsDto>
{
    public Guid Id { get; set; }

    public GetTeamRequest(Guid id) => Id = id;
}

public class GetTeamRequestHandler : IRequestHandler<GetTeamRequest, TeamDetailsDto>
{
    private readonly IRepository<Team> _repository;
    private readonly IStringLocalizer _t;

    public GetTeamRequestHandler(IRepository<Team> repository, IStringLocalizer<GetTeamRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);
     public async Task<TeamDetailsDto> Handle(GetTeamRequest request, CancellationToken cancellationToken) =>
 await _repository.GetBySpecAsync(
 (ISpecification<Team, TeamDetailsDto>)new TeamByIdWithGameSpec(request.Id), cancellationToken)
 ?? throw new NotFoundException(_t["Team {0} Not Found.", request.Id]);


  
}