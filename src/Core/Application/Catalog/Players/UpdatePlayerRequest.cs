using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Players;

public class UpdatePlayerRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
   public string Name { get; set; } = default!;
	public string? Phone { get; set; }
	public byte Age { get; set; }
	public byte Level { get; set; }
	public DefaultIdType UserId { get; set; }
	public DefaultIdType TeamId { get; set; }
    public DefaultIdType GameId { get; set; }

}

public class UpdatePlayerRequestHandler : IRequestHandler<UpdatePlayerRequest, Guid>
{
    private readonly IRepository<Player> _repository;
    private readonly IStringLocalizer _t;
   

    public UpdatePlayerRequestHandler(IRepository<Player> repository, IStringLocalizer<UpdatePlayerRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdatePlayerRequest request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = player ?? throw new NotFoundException(_t["Player {0} Not Found.", request.Id]);

               
        
        var updatedPlayer = player.Update(request.Name, request.Phone, request.Age, request.Level, request.UserId, request.TeamId,request.GameId);
        
        // Add Domain Events to be raised after the commit
        player.DomainEvents.Add(EntityUpdatedEvent.WithEntity(player));

        await _repository.UpdateAsync(updatedPlayer, cancellationToken);

        return request.Id;
    }
}