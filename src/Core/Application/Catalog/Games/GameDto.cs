
namespace FSH.WebApi.Application.Catalog.Games;

public class GameDto : IDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public DateTime DateTime { get; set; }	
			
	
}