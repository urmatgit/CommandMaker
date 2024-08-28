namespace FSH.WebApi.Application.Catalog.Players;

public class PlayersByTeamSpec : Specification<Player>
{
    public PlayersByTeamSpec(Guid teamId) =>
        Query.Where(p => p.TeamId == teamId);
}
