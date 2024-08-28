using FSH.WebApi.Application.Catalog.Games;

namespace FSH.WebApi.Application.Catalog.Teams;

public class CreateTeamRequestValidator : CustomValidator<CreateTeamRequest>
{
    public CreateTeamRequestValidator(IReadRepository<Team> teamRepo, IReadRepository<Game>gameRepo, IStringLocalizer<CreateTeamRequestValidator> localizer)
    {
    // Important leftout the T for the translations !
    
        RuleFor(p => p.Name)
            .NotEmpty()            
            .MustAsync(async (name, ct) => await teamRepo.GetBySpecAsync(new TeamByNameSpec(name), ct) is null)
            .WithMessage((_, name) => "Team {0} already Exists.");

          RuleFor(p => p.GameId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await gameRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => "Game {0} Not Found.");
	 
    }
}