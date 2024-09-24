namespace FSH.WebApi.Application.Catalog.Teams;

public class TeamsBySearchRequestWithGamesSpec : EntitiesByPaginationFilterSpec<Team, TeamDto>
{
//    replace <&Orderby&> with fieldname and uncomment code.
    public TeamsBySearchRequestWithGamesSpec(SearchTeamsRequest request,Guid onlyMy)
        : base(request) =>
        Query
            .Include(p => p.Game)
//            .OrderBy(c => c.<&Orderby&>, !request.HasOrderBy())
            .ThenInclude(p => p.CreatedBy==onlyMy,onlyMy!=Guid.Empty  )
            .Where(p => p.GameId.Equals(request.GameId!.Value), request.GameId.HasValue);
}
            