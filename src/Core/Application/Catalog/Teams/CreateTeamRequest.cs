

using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Teams;

public class CreateTeamRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
	public string Captain { get; set; } = default!;
	public DefaultIdType UserId { get; set; }
	public string? Description { get; set; }
	public DefaultIdType GameId { get; set; }
    
}

public class CreateTeamRequestHandler : IRequestHandler<CreateTeamRequest, Guid>
{
    private readonly IRepository<Team> _repository;
    
    public CreateTeamRequestHandler(IRepository<Team> repository) =>
        _repository = repository;

    public async Task<Guid> Handle(CreateTeamRequest request, CancellationToken cancellationToken)
    {
        var team = new Team(request.Name, request.Captain, request.UserId, request.Description, request.GameId);

        // Add Domain Events to be raised after the commit
        team.DomainEvents.Add(EntityCreatedEvent.WithEntity(team));

        await _repository.AddAsync(team, cancellationToken);

        return team.Id;
    }
}