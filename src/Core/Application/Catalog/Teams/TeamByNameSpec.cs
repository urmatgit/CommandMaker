namespace FSH.WebApi.Application.Catalog.Teams;

public class TeamByNameSpec : Specification<Team>, ISingleResultSpecification
{
    public TeamByNameSpec(string Name) =>
        Query.Where(p => p.Name == Name);
}