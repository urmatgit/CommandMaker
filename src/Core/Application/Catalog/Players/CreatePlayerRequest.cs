

using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Players;

public class CreatePlayerRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
	public string? Phone { get; set; }
	public byte Age { get; set; }
	public byte Level { get; set; }
	public DefaultIdType UserId { get; set; }
	public DefaultIdType TeamId { get; set; }
    public DefaultIdType GameId { get; set; }

}

public class CreatePlayerRequestHandler : IRequestHandler<CreatePlayerRequest, Guid>
{
    private readonly IRepository<Player> _repository;
    
    public CreatePlayerRequestHandler(IRepository<Player> repository) =>
        _repository = repository;

    public async Task<Guid> Handle(CreatePlayerRequest request, CancellationToken cancellationToken)
    {
        var player = new Player(request.Name, request.Phone, request.Age, request.Level, request.UserId, request.TeamId,request.GameId);

        // Add Domain Events to be raised after the commit
        player.DomainEvents.Add(EntityCreatedEvent.WithEntity(player));

        await _repository.AddAsync(player, cancellationToken);

        return player.Id;
    }
}