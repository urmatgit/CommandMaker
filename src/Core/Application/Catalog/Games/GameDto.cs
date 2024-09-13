
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FSH.WebApi.Application.Catalog.Games;

public class GameDto : IDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public string? Description { get; set; }

	public DateTime? DateTime { get; set; }
    public bool CurrentUserIn { get; set; }
    public TimeSpan? Time {
        get
        {
            return DateTime.Value.TimeOfDay; }
        set {
            if (value.HasValue)
            {
               DateTime =new DateTime(DateTime.Value.Year, DateTime.Value.Month, DateTime.Value.Day, Time.Value.Hours, Time.Value.Minutes, Time.Value.Seconds);
            }else 
                DateTime = new DateTime(DateTime.Value.Year, DateTime.Value.Month, DateTime.Value.Day); 
        }
    }
    
     
}