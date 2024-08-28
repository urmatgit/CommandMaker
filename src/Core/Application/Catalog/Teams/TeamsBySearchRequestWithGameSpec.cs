namespace FSH.WebApi.Application.Catalog.Teams;

public class TeamsBySearchRequestWithGamesSpec : EntitiesByPaginationFilterSpec<Team, TeamDto>
{
//    replace <&Orderby&> with fieldname and uncomment code.
    public TeamsBySearchRequestWithGamesSpec(SearchTeamsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Game)
//            .OrderBy(c => c.<&Orderby&>, !request.HasOrderBy())
            .Where(p => p.GameId.Equals(request.GameId!.Value), request.GameId.HasValue);
}
            