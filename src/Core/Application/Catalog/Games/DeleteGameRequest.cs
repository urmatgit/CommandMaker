

namespace FSH.WebApi.Application.Catalog.Games;

public class DeleteGameRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public DeleteGameRequest(Guid id) => Id = id;
}

public class DeleteGameRequestHandler : IRequestHandler<DeleteGameRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents

    private readonly IRepositoryWithEvents<Game> _gameRepo;
     
   private readonly IStringLocalizer _t;


    public DeleteGameRequestHandler(IRepositoryWithEvents<Game> gameRepo, IStringLocalizer<DeleteGameRequestHandler> localizer) =>
        (_gameRepo, _t) = (gameRepo, localizer);

    
    public async Task<Guid> Handle(DeleteGameRequest request, CancellationToken cancellationToken)
    {
     
    
     var game = await _gameRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = game ?? throw new NotFoundException(_t["Game.notfound"]);

        await _gameRepo.DeleteAsync(game, cancellationToken);

        return request.Id;
    }
}