namespace FSH.WebApi.Application.Catalog.Teams;

public class TeamsByGameSpec : Specification<Team>
{
    public TeamsByGameSpec(Guid gameId) =>
        Query.Where(p => p.GameId == gameId);
}
