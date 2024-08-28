
namespace FSH.WebApi.Application.Catalog.Players;

public class PlayerDto : IDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public string? Phone { get; set; }
	public byte Age { get; set; }
	public byte Level { get; set; }
	public DefaultIdType UserId { get; set; }
	public DefaultIdType TeamId { get; set; }	
			
	public string TeamName { get; set; } = default!;
}