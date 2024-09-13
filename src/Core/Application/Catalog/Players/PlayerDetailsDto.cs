using FSH.WebApi.Application.Catalog.Teams;

namespace FSH.WebApi.Application.Catalog.Players;

public class PlayerDetailsDto : IDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public string? Phone { get; set; }
	public byte Age { get; set; }
	public byte Level { get; set; }
	public DefaultIdType UserId { get; set; }
	public DefaultIdType? TeamId { get; set; }
    public DefaultIdType? GameId { get; set; }
    public string?  TeamName { get; set; } = default!;
    public string? GameName { get; set; } = default!;
}


