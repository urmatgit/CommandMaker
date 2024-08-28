using Mapster;

namespace FSH.WebApi.Application.Catalog.Players;

public class GetPlayerViaDapperRequest : IRequest<PlayerDto>
{
    public Guid Id { get; set; }

    public GetPlayerViaDapperRequest(Guid id) => Id = id;
}

public class GetPlayerViaDapperRequestHandler : IRequestHandler<GetPlayerViaDapperRequest, PlayerDto>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer<GetPlayerViaDapperRequestHandler> _t;

    public GetPlayerViaDapperRequestHandler(IDapperRepository repository, IStringLocalizer<GetPlayerViaDapperRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<PlayerDto> Handle(GetPlayerViaDapperRequest request, CancellationToken cancellationToken)
    {
        var player = await _repository.QueryFirstOrDefaultAsync<Player>(
            $"SELECT * FROM public.\"Players\" WHERE \"Id\"  = '{request.Id}' AND \"Tenant\" = '@tenant'", cancellationToken: cancellationToken);

                _ = player ?? throw new NotFoundException(_t["Player {0} Not Found.", request.Id]);

        return player.Adapt<PlayerDto>();


    }
}