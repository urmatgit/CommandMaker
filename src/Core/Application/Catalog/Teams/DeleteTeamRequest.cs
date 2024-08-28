using FSH.WebApi.Domain.Common.Events;
namespace FSH.WebApi.Application.Catalog.Teams;


public class DeleteTeamRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteTeamRequest(Guid id) => Id = id;
}

public class DeleteTeamRequestHandler : IRequestHandler<DeleteTeamRequest, Guid>
{
    private readonly IRepository<Team> _repository;
    private readonly IStringLocalizer<DeleteTeamRequestHandler> _t;

    public DeleteTeamRequestHandler(IRepository<Team> repository, IStringLocalizer<DeleteTeamRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _repository.GetByIdAsync(request.Id, cancellationToken);

                _ = team ?? throw new NotFoundException(_t["Team {0} Not Found."]);


        // Add Domain Events to be raised after the commit
        team.DomainEvents.Add(EntityDeletedEvent.WithEntity(team));

        await _repository.DeleteAsync(team, cancellationToken);

        return request.Id;
    }
}