namespace FSH.WebApi.Application.Common.Specification;

public class AuditableEntitiesByCreatedOnBetweenSpec<T> : Specification<T>
    where T : AuditableEntity
{
    public AuditableEntitiesByCreatedOnBetweenSpec(DateTime from, DateTime until) =>
        Query.Where(e => e.CreatedOn >= from && e.CreatedOn <= until);
}
public class AuditableGameByCreatedOnBetweenSpec : AuditableEntitiesByCreatedOnBetweenSpec<Game>
{
    public AuditableGameByCreatedOnBetweenSpec(Guid creatord, DateTime from, DateTime until): base(from, until)
    {
        Query.Where(e => e.CreatedBy==creatord,creatord!=Guid.Empty);
    } 
        
}
public class AuditableTeamByCreatedOnBetweenSpec : AuditableEntitiesByCreatedOnBetweenSpec<Team>
{
    public AuditableTeamByCreatedOnBetweenSpec(Guid creatord, DateTime from, DateTime until) : base(from, until)
    {
        Query
            .Include(g=>g.Game)
            .Where(e => e.Game.CreatedBy == creatord, creatord != Guid.Empty);
    }

}
public class AuditablePlayerByCreatedOnBetweenSpec : AuditableEntitiesByCreatedOnBetweenSpec<Player>
{
    public AuditablePlayerByCreatedOnBetweenSpec(Guid creatord, DateTime from, DateTime until) : base(from, until)
    {
        Query
            .Include(g => g.Team)
            .ThenInclude(t=>t.Game)
            .Where(e => e.Team.Game.CreatedBy == creatord, creatord != Guid.Empty);
    }

}