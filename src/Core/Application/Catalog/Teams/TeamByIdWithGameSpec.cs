namespace FSH.WebApi.Application.Catalog.Teams;

public class TeamByIdWithGameSpec : Specification<Team, TeamDetailsDto>, ISingleResultSpecification

{
    public TeamByIdWithGameSpec(Guid id) =>
        Query
        .Where(t => t.Id == id)
	.Include(t => t.Game)
		;
        
}
