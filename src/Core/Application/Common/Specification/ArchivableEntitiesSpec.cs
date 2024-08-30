using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Common.Specification;
public class ArchivableEntitiesSpec: Specification<Game>
{   
    public ArchivableEntitiesSpec(Guid userid)
    {
        Query
            .Where(e => e.CreatedBy==userid,userid!=Guid.Empty);
    }
}

public class TeamCountSpec : Specification<Team>
{
    public TeamCountSpec(Guid userid)
    {
        Query
            .Include(g => g.Game)
            .Where(x => x.Game.CreatedBy == userid, userid != Guid.Empty);
            
            
    }
}
public class PlayerCountSpec : Specification<Player>
{
    public PlayerCountSpec(Guid userid)
    {
        Query
            .Include(t => t.Team)
            .ThenInclude(g=>g.Game)
            .Where(x => x.Team.Game.CreatedBy == userid, userid != Guid.Empty);

    }
}

//<T> : Specification<T>
//    where T : AuditableEntity
//{
//    public AuditableEntitiesByCreatedOnBetweenSpec(DateTime from, DateTime until) =>
//        Query.Where(e => e.CreatedOn >= from && e.CreatedOn <= until);
//}