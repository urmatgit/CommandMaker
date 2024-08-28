using FSH.WebApi.Application.Catalog.Players;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class PlayersController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Players)]
    [OpenApiOperation("Search Players using available filters.", "")]
    public Task<PaginationResponse<PlayerDto>> SearchAsync(SearchPlayersRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Players)]
    [OpenApiOperation("Get player details.", "")]
    public Task<PlayerDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetPlayerRequest(id));
    }

    [HttpGet("dapper")]
    [MustHavePermission(FSHAction.View, FSHResource.Players)]
    [OpenApiOperation("Get player details via dapper.", "")]
    public Task<PlayerDto> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new GetPlayerViaDapperRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Players)]
    [OpenApiOperation("Create a new player.", "")]
    public Task<Guid> CreateAsync(CreatePlayerRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Players)]
    [OpenApiOperation("Update a player.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdatePlayerRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Players)]
    [OpenApiOperation("Delete a player.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeletePlayerRequest(id));
    }

    }