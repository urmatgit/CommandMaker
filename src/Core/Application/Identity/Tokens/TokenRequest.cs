using FSH.WebApi.Shared.Constants;
using System.Text.RegularExpressions;

namespace FSH.WebApi.Application.Identity.Tokens;

public record TokenRequest(string username, string Password);

public class TokenRequestValidator : CustomValidator<TokenRequest>
{
    public TokenRequestValidator(IStringLocalizer<TokenRequestValidator> T)
    {
        RuleFor(p => p.username)
            .NotEmpty();
        //RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .EmailAddress()
        //        .WithMessage(T["Invalid Email Address."]);

     //   RuleFor(p => p.username).Cascade(CascadeMode.Stop)
     //     .NotEmpty()
     //     .NotNull().WithMessage("Phone Number is required.")
     //.MinimumLength(8).WithMessage("PhoneNumber must not be less than 11 characters.")
     //.MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
     //.Matches(new Regex(HelpersConstants.PhoneNumberRegularExpression)).WithMessage("PhoneNumber not valid");
        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}