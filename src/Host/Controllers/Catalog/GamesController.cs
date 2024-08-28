using FSH.WebApi.Application.Catalog.Games;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class GamesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Games)]
    [OpenApiOperation("Search games using available filters.", "")]
    public Task<PaginationResponse<GameDto>> SearchAsync(SearchGamesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Games)]
    [OpenApiOperation("Get game details.", "")]
    public Task<GameDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetGameRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Games)]
    [OpenApiOperation("Create a new game.", "")]
    public Task<Guid> CreateAsync(CreateGameRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Games)]
    [OpenApiOperation("Update a game.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateGameRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Games)]
    [OpenApiOperation("Delete a game.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteGameRequest(id));
    }    
}