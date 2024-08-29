using FSH.WebApi.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Catalog.Teams;



public class GetTeamsByGameIdRequest : IRequest<List<TeamDto>>
{
    public DefaultIdType GameId { get; set; }
    public GetTeamsByGameIdRequest(DefaultIdType gameid)=>GameId = gameid;
    
}

public class EntityBySearchRequestSpec : Specification<Team,TeamDto>
{
    public EntityBySearchRequestSpec(GetTeamsByGameIdRequest request)
        => Query.Where(x => x.GameId == request.GameId);

}

public class GetTeamsByGameIdRequestHandler : IRequestHandler<GetTeamsByGameIdRequest, List<TeamDto>>
{
    private readonly IReadRepository<Team> _repository;

    public GetTeamsByGameIdRequestHandler(IReadRepository<Team> repository) => _repository = repository;

    public async Task<List<TeamDto>> Handle(GetTeamsByGameIdRequest request, CancellationToken cancellationToken)
    {
        var spec = new EntityBySearchRequestSpec(request);
        return await _repository.ListAsync<TeamDto>(spec,  cancellationToken);
    }
}


