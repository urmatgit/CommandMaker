namespace FSH.WebApi.Application.Catalog.Games;

public class GameByNameSpec : Specification<Game>, ISingleResultSpecification
{
    public GameByNameSpec(string Name) =>
        Query.Where(p => p.Name == Name);
}