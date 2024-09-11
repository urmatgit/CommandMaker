using FSH.WebApi.Shared.Constants;
using System.Text.RegularExpressions;

namespace FSH.WebApi.Application.Identity.Users;

public class UpdateUserRequestValidator : CustomValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator(IUserService userService, IStringLocalizer<UpdateUserRequestValidator> T)
    {
        RuleFor(p => p.Id)
            .NotEmpty();

        //RuleFor(p => p.FirstName)
        //    .NotEmpty()
        //    .MaximumLength(75);

        //RuleFor(p => p.LastName)
        //    .NotEmpty()
        //    .MaximumLength(75);

        RuleFor(p => p.Email)
            //.NotEmpty()
            //.EmailAddress()
            //    .WithMessage(T["Invalid Email Address."])
            .MustAsync(async (user, email, _) => !await userService.ExistsWithEmailAsync(email, user.Id))
                .WithMessage((_, email) => string.Format(T["Email {0} is already registered."], email));

        RuleFor(p => p.Image);

        RuleFor(u => u.PhoneNumber).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
       .MinimumLength(8).WithMessage("PhoneNumber must not be less than 10 characters.")
       .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
       .Matches(new Regex(HelpersConstants.PhoneNumberRegularExpression)).WithMessage("PhoneNumber not valid")
            .MustAsync(async (user, phone, _) => !await userService.ExistsWithPhoneNumberAsync(phone!, user.Id))
                .WithMessage((_, phone) => string.Format(T["Phone number {0} is already registered."], phone))
                .Unless(u => string.IsNullOrWhiteSpace(u.PhoneNumber));
        RuleFor(p => p.BirthDate).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .InclusiveBetween(new DateTime(1900, 01, 01), DateTime.Now)
            .WithMessage("The birth date is incorrect");
    }
}