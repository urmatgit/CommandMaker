namespace FSH.WebApi.Application.Catalog.Players;

public class UpdatePlayerRequestValidator : CustomValidator<UpdatePlayerRequest>
{
    public UpdatePlayerRequestValidator(IReadRepository<Player> playerRepo, IReadRepository<Team>teamRepo, IStringLocalizer<UpdatePlayerRequestValidator> localizer)
    {

    // replace validator_<&validator_name&> with appropriate field<&validator_name&>  !! Watch out for lower and Capital and field type
    // replace or remove eventual length in <&validator_length&>
    // sample code if <&validator_name&> would be a rule  (to be altered accordingly your specific rules)

    //    RuleFor(p => p.<&validator_Name&>)
    //        .NotEmpty()
    //        .MaximumLength(<&validator_length&>)
    //        .MustAsync(async (<&validator_name&>, ct) => await playerRepo.GetBySpecAsync(new PlayerByNameSpec(<&validator_name&>), ct) is null)    
    //        .WithMessage((_, <&validator_name&>) => T["Player {0} already Exists.", <&validator_name&>]);

                       
       
    }
}