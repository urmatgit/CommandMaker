namespace FSH.WebApi.Application.Catalog.Teams;

public class UpdateTeamRequestValidator : CustomValidator<UpdateTeamRequest>
{
    public UpdateTeamRequestValidator(IReadRepository<Team> teamRepo, IReadRepository<Game>gameRepo, IStringLocalizer<UpdateTeamRequestValidator> localizer)
    {

    // replace validator_<&validator_name&> with appropriate field<&validator_name&>  !! Watch out for lower and Capital and field type
    // replace or remove eventual length in <&validator_length&>
    // sample code if <&validator_name&> would be a rule  (to be altered accordingly your specific rules)

    //    RuleFor(p => p.<&validator_Name&>)
    //        .NotEmpty()
    //        .MaximumLength(<&validator_length&>)
    //        .MustAsync(async (<&validator_name&>, ct) => await teamRepo.GetBySpecAsync(new TeamByNameSpec(<&validator_name&>), ct) is null)    
    //        .WithMessage((_, <&validator_name&>) => T["Team {0} already Exists.", <&validator_name&>]);

                       
       
    }
}