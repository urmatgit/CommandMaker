using FSH.WebApi.Application.Identity.Roles;
using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Shared.Authorization;

namespace FSH.WebApi.Application.Dashboard;

public class GetStatsRequest : IRequest<StatsDto>
{
}

public class GetStatsRequestHandler : IRequestHandler<GetStatsRequest, StatsDto>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IReadRepository<Team> _TeamRepo;
    private readonly IReadRepository<Player> _playerRepo;
    private readonly IReadRepository<Game> _gameRepo;
    private readonly IStringLocalizer _t;
    private readonly ICurrentUser _currentUser;
    public GetStatsRequestHandler(ICurrentUser currentUser, IUserService userService, IRoleService roleService,IReadRepository<Game> gameRepo,  IReadRepository<Team> brandRepo, IReadRepository<Player> productRepo, IStringLocalizer<GetStatsRequestHandler> localizer)
    {
        _currentUser   = currentUser;
        _userService = userService;
        _roleService = roleService;
        _TeamRepo = brandRepo;
        _playerRepo = productRepo;
        _gameRepo = gameRepo;
        _t = localizer;
    }

    public async Task<StatsDto> Handle(GetStatsRequest request, CancellationToken cancellationToken)
    {

        var currentUserId = _currentUser.GetUserId();
        
        var viewallPermission = await _userService.HasPermissionAsync(currentUserId.ToString(), FSHPermission.NameFor(FSHAction.ViewAll, FSHResource.Games));
        viewallPermission = viewallPermission || await _userService.IsInRoleAsync(currentUserId.ToString(), FSHRoles.Basic, cancellationToken);
        if (viewallPermission) currentUserId = Guid.Empty;
        var stats = new StatsDto
        {
            GameCount=await _gameRepo.CountAsync(new  ArchivableEntitiesSpec(currentUserId), cancellationToken),
            PlayerCount = await _playerRepo.CountAsync(new PlayerCountSpec(currentUserId),cancellationToken),
            TeamCount = await _TeamRepo.CountAsync(new TeamCountSpec(currentUserId),cancellationToken),
            UserCount = await _userService.GetCountAsync(cancellationToken),
            RoleCount = await _roleService.GetCountAsync(cancellationToken)
        };

        int selectedYear = DateTime.UtcNow.Year;
        double[] productsFigure = new double[13];
        double[] brandsFigure = new double[13];
        double[] gamesFigure = new double[13];
        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01).ToUniversalTime();
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59).ToUniversalTime(); // Monthly Based

            var teamSpec = new AuditableTeamByCreatedOnBetweenSpec(currentUserId, filterStartDate, filterEndDate);
            var playerSpec = new AuditablePlayerByCreatedOnBetweenSpec(currentUserId, filterStartDate, filterEndDate);
            var gamesSpec=new AuditableGameByCreatedOnBetweenSpec(currentUserId,filterStartDate, filterEndDate);
            brandsFigure[i - 1] = await _TeamRepo.CountAsync(teamSpec, cancellationToken);
            productsFigure[i - 1] = await _playerRepo.CountAsync(playerSpec, cancellationToken);
            gamesFigure[i - 1] = await _gameRepo.CountAsync(gamesSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Players"], Data = productsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Teams"], Data = brandsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Games"], Data = gamesFigure });

        return stats;
    }
}
