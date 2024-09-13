using FSH.WebApi.Application.Catalog.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Catalog.Games;
public class PlayersByUserIdAndGameIdSpec : Specification<Player>
{
    public PlayersByUserIdAndGameIdSpec(Guid UserId, Guid GameId) =>
        Query.Where(p => p.UserId == UserId,UserId!=Guid.Empty)
        .Where(p=>p.GameId == GameId,GameId!=Guid.Empty);
}