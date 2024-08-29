using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Teams;

public class UpdateTeamRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
   public string Name { get; set; } = default!;
	public string Captain { get; set; } = default!;
	public DefaultIdType UserId { get; set; }
	public string? Description { get; set; }
	public DefaultIdType GameId { get; set; }
   
}

public class UpdateTeamRequestHandler : IRequestHandler<UpdateTeamRequest, Guid>
{
    private readonly IRepository<Team> _repository;
    private readonly IStringLocalizer _t;
   

    public UpdateTeamRequestHandler(IRepository<Team> repository, IStringLocalizer<UpdateTeamRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = team ?? throw new NotFoundException(_t["Team {0} Not Found.", request.Id]);

               
        
        var updatedTeam = team.Update(request.Name, request.Captain, request.Description, request.UserId, request.GameId);

        // Add Domain Events to be raised after the commit
        team.DomainEvents.Add(EntityUpdatedEvent.WithEntity(team));

        await _repository.UpdateAsync(updatedTeam, cancellationToken);

        return request.Id;
    }
}