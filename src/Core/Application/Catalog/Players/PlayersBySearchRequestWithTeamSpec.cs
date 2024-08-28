namespace FSH.WebApi.Application.Catalog.Players;

public class PlayersBySearchRequestWithTeamsSpec : EntitiesByPaginationFilterSpec<Player, PlayerDto>
{
//    replace <&Orderby&> with fieldname and uncomment code.
    public PlayersBySearchRequestWithTeamsSpec(SearchPlayersRequest request)
        : base(request) =>
        Query
            .Include(p => p.Team)
//            .OrderBy(c => c.<&Orderby&>, !request.HasOrderBy())
            .Where(p => p.TeamId.Equals(request.TeamId!.Value), request.TeamId.HasValue);
}
            