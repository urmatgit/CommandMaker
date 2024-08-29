using FSH.WebApi.Application.Catalog.Teams;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class TeamsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Teams)]
    [OpenApiOperation("Search Teams using available filters.", "")]
    public Task<PaginationResponse<TeamDto>> SearchAsync(SearchTeamsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Teams)]
    [OpenApiOperation("Get team details.", "")]
    public Task<TeamDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetTeamRequest(id));
    }
    [HttpGet("getbygameid/{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Teams)]
    [OpenApiOperation("Get team by game id.", "")]
    public async Task<List<TeamDto>> GetByGameIdAsync(Guid id)
    {
        return await Mediator.Send(new GetTeamsByGameIdRequest(id));
    }
    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Teams)]
    [OpenApiOperation("Get team details via dapper.", "")]
    public Task<TeamDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new GetTeamViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Teams)]
    [OpenApiOperation("Create a new team.", "")]
    public Task<Guid> CreateAsync(CreateTeamRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Teams)]
    [OpenApiOperation("Update a team.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateTeamRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Teams)]
    [OpenApiOperation("Delete a team.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteTeamRequest(id));
    }

    }