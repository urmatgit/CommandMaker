using FSH.WebApi.Application.Catalog.Games;

namespace FSH.WebApi.Application.Catalog.Teams;

public class TeamDetailsDto : IDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public string Captain { get; set; } = default!;
	public DefaultIdType UserId { get; set; }
	public string? Description { get; set; }
	public DefaultIdType GameId { get; set; }		
	public GameDto Game { get; set; } = default!;
}


