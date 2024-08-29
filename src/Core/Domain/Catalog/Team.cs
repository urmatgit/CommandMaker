using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Catalog;
public class Team : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }= default!;
    public string Captain {  get; private set; }=default!;
    public string? Description { get; private set; }
    //Captain userid
    public DefaultIdType UserId { get; private set; } = default!;
    
    public DefaultIdType GameId { get; private set; }
    public virtual Game Game { get; private set; }=default!;
         

    public Team(string name, string captain,DefaultIdType userId,  string? description,DefaultIdType gameId)
    {
        Name = name;
        Description = description;
        Captain = captain;
        UserId = userId;
        GameId = gameId;
    }

    public Team Update(string? name, string captain, string? description , DefaultIdType userid,  DefaultIdType gameId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (captain is not null && Captain.Equals(captain) is not true) Captain = captain;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (GameId!=gameId) GameId= gameId;
        if (UserId != userid) UserId = userid;
        return this;
    }
}
