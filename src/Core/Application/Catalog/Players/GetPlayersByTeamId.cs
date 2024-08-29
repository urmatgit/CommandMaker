using FSH.WebApi.Application.Catalog.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Catalog.Players;
internal class GetPlayersByPlayerId
{
}

public class GetPlayersByTeamIdRequest : IRequest<List<PlayerDto>>
{
    public DefaultIdType TeamId { get; set; }
    public GetPlayersByTeamIdRequest(DefaultIdType teamId)
    {
        TeamId = teamId;
    }
}

public class EntityBySearchRequestSpec : Specification<Player, PlayerDto>
{
    public EntityBySearchRequestSpec(GetPlayersByTeamIdRequest request)
        => Query.Where(x => x.TeamId == request.TeamId);

}

public class GetPlayersByTeamIdRequestHandler : IRequestHandler<GetPlayersByTeamIdRequest, List<PlayerDto>>
{
    private readonly IReadRepository<Player> _repository;

    public GetPlayersByTeamIdRequestHandler(IReadRepository<Player> repository) => _repository = repository;

    public async Task<List<PlayerDto>> Handle(GetPlayersByTeamIdRequest request, CancellationToken cancellationToken)
    {
        var spec = new EntityBySearchRequestSpec(request);
        return await _repository.ListAsync<PlayerDto>(spec, cancellationToken);
    }
}

