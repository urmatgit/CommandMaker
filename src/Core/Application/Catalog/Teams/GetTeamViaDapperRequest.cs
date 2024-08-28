using Mapster;

namespace FSH.WebApi.Application.Catalog.Teams;

public class GetTeamViaDapperRequest : IRequest<TeamDto>
{
    public Guid Id { get; set; }

    public GetTeamViaDapperRequest(Guid id) => Id = id;
}

public class GetTeamViaDapperRequestHandler : IRequestHandler<GetTeamViaDapperRequest, TeamDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<GetTeamViaDapperRequestHandler> _t;

    public GetTeamViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<GetTeamViaDapperRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<TeamDto> Handle(GetTeamViaDapperRequest request, CancellationToken cancellationToken)
    {
        var team = await _repository.QueryFirstOrDefaultAsync<Team>(
            $"SELECT * FROM public.\"Teams\" WHERE \"Id\"  = '{request.Id}' AND \"Tenant\" = '@tenant'", cancellationToken: cancellationToken);

                _ = team ?? throw new NotFoundException(_t["Team {0} Not Found.", request.Id]);

        return team.Adapt<TeamDto>();


    }
}