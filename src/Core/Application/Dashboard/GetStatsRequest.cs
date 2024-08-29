﻿using FSH.WebApi.Application.Identity.Roles;
using FSH.WebApi.Application.Identity.Users;

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
    private readonly IStringLocalizer _t;

    public GetStatsRequestHandler(IUserService userService, IRoleService roleService, IReadRepository<Team> brandRepo, IReadRepository<Player> productRepo, IStringLocalizer<GetStatsRequestHandler> localizer)
    {
        _userService = userService;
        _roleService = roleService;
        _TeamRepo = brandRepo;
        _playerRepo = productRepo;
        _t = localizer;
    }

    public async Task<StatsDto> Handle(GetStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new StatsDto
        {
            PlayerCount = await _playerRepo.CountAsync(cancellationToken),
            TeamCount = await _TeamRepo.CountAsync(cancellationToken),
            UserCount = await _userService.GetCountAsync(cancellationToken),
            RoleCount = await _roleService.GetCountAsync(cancellationToken)
        };

        int selectedYear = DateTime.UtcNow.Year;
        double[] productsFigure = new double[13];
        double[] brandsFigure = new double[13];
        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01).ToUniversalTime();
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59).ToUniversalTime(); // Monthly Based

            var brandSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Team>(filterStartDate, filterEndDate);
            var productSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Player>(filterStartDate, filterEndDate);

            brandsFigure[i - 1] = await _TeamRepo.CountAsync(brandSpec, cancellationToken);
            productsFigure[i - 1] = await _playerRepo.CountAsync(productSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Players"], Data = productsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Teams"], Data = brandsFigure });

        return stats;
    }
}
