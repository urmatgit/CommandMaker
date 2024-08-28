using FSH.WebApi.Domain.Common.Events;
namespace FSH.WebApi.Application.Catalog.Players;


public class DeletePlayerRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeletePlayerRequest(Guid id) => Id = id;
}

public class DeletePlayerRequestHandler : IRequestHandler<DeletePlayerRequest, Guid>
{
    private readonly IRepository<Player> _repository;
    private readonly IStringLocalizer<DeletePlayerRequestHandler> _t;

    public DeletePlayerRequestHandler(IRepository<Player> repository, IStringLocalizer<DeletePlayerRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeletePlayerRequest request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id, cancellationToken);

                _ = player ?? throw new NotFoundException(_t["Player {0} Not Found."]);


        // Add Domain Events to be raised after the commit
        player.DomainEvents.Add(EntityDeletedEvent.WithEntity(player));

        await _repository.DeleteAsync(player, cancellationToken);

        return request.Id;
    }
}