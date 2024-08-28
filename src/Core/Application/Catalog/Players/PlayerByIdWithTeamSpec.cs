namespace FSH.WebApi.Application.Catalog.Players;

public class PlayerByIdWithTeamSpec : Specification<Player, PlayerDetailsDto>, ISingleResultSpecification

{
    public PlayerByIdWithTeamSpec(Guid id) =>
        Query
        .Where(p => p.Id == id)
	.Include(p => p.Team)
		;
        
}
