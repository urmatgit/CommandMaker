using FSH.WebApi.Application.Catalog.Teams;

namespace FSH.WebApi.Application.Catalog.Players;

public class CreatePlayerRequestValidator : CustomValidator<CreatePlayerRequest>
{
    public CreatePlayerRequestValidator(IReadRepository<Player> playerRepo, IReadRepository<Team>teamRepo, IStringLocalizer<CreatePlayerRequestValidator> localizer)
    {
    // Important leftout the T for the translations !
    
        RuleFor(p => p.Name)
            .NotEmpty()            
            .MustAsync(async (name, ct) => await playerRepo.GetBySpecAsync(new PlayerByNameSpec(name), ct) is null)
            .WithMessage((_, name) => "Player {0} already Exists.");

          //RuleFor(p => p.TeamId)
          //  //.NotEmpty()
          //  .MustAsync(async (id, ct) => await teamRepo.GetByIdAsync(id, ct) is not null)
          //  .WithMessage((_, id) => "Team {0} Not Found.");
	 
    }
}