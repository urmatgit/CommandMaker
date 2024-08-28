namespace FSH.WebApi.Application.Catalog.Players;

public class PlayerByNameSpec : Specification<Player>, ISingleResultSpecification
{
    public PlayerByNameSpec(string Name) =>
        Query.Where(p => p.Name == Name);
}