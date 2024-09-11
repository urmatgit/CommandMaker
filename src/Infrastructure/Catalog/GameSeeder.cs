using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.Catalog;
public class GameSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<GameSeeder> _logger;
    private readonly ICurrentUser _currentUser;
    public GameSeeder(ISerializerService serializerService,ApplicationDbContext applicationDbContext, ILogger<GameSeeder> logger,ICurrentUser currentUser )
    {
        _serializerService = serializerService;
        _db = applicationDbContext;
        _logger = logger;
        _currentUser = currentUser;
    }
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        Game game = null;
        if (! await _db.Games.AnyAsync(cancellationToken)) {
             game = new Game("Test my game", "", DateTime.UtcNow);
            await _db.Games.AddAsync(game,cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seed game.");
        }
        Team team = null;
        if (!await _db.Teams.AnyAsync(cancellationToken)) {
            game=game ?? await _db.Games.FirstOrDefaultAsync(cancellationToken);

            team = new Team("My team", "Captain 1", Guid.Empty,"This is test team", game.Id);
            await _db.Teams.AddAsync(team,cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seed team.");
        }
        if (!await _db.Players.AnyAsync(cancellationToken))
        {
            team = team ?? await _db.Teams.FirstOrDefaultAsync(cancellationToken);
            List<Player> players = new List<Player>();
            for (int i = 1; i < 7; i++) {
                var player = new Player($"Player {i}", $"+7950000000{i}", (byte)(14 + i), 0, Guid.Empty, team.Id, game.Id);
                players.Add(player);
            }
            await _db.Players.AddRangeAsync(players,cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seed players.");
        }

    }
}
