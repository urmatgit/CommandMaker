namespace FSH.WebApi.Application.Dashboard;

public class StatsDto
{
    public int GameCount {  get; set; }
    public int PlayerCount { get; set; }
    public int TeamCount { get; set; }
    public int UserCount { get; set; }
    public int RoleCount { get; set; }
    public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    public Dictionary<string, double>? ProductByBrandTypePieChart { get; set; }
}

public class ChartSeries
{
    public string? Name { get; set; }
    public double[]? Data { get; set; }
}