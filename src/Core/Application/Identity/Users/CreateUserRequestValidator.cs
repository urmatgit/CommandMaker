using FluentValidation;
using FSH.WebApi.Shared.Constants;
using System.Text.RegularExpressions;

namespace FSH.WebApi.Application.Identity.Users;

public class CreateUserRequestValidator : CustomValidator<CreateUserRequest>
{
    public CreateUserRequestValidator(IUserService userService, IStringLocalizer<CreateUserRequestValidator> T)
    {
        RuleFor(u => u.Email).Cascade(CascadeMode.Stop)
            //.NotEmpty()
            //.EmailAddress()
            //    .WithMessage(T["Invalid Email Address."])
            .MustAsync(async (email, _) => !await userService.ExistsWithEmailAsync(email))
                .WithMessage((_, email) => T["Email {0} is already registered.", email]);
        
        RuleFor(u => u.UserName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(6)
            .MustAsync(async (name, _) => !await userService.ExistsWithNameAsync(name))
                .WithMessage((_, name) => T["Username {0} is already taken.", name]);

        RuleFor(u => u.PhoneNumber).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
       .MinimumLength(8).WithMessage("PhoneNumber must not be less than 11 characters.")
       .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
       .Matches(new Regex(HelpersConstants.PhoneNumberRegularExpression)).WithMessage("PhoneNumber not valid")
           .MustAsync(async (phone, _) => !await userService.ExistsWithPhoneNumberAsync(phone!))
                .WithMessage((_, phone) => T["Phone number {0} is already registered.", phone!]);
                //.Unless(u => string.IsNullOrWhiteSpace(u.PhoneNumber));

        RuleFor(p => p.FirstName).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(p => p.LastName).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(p => p.ConfirmPassword).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Equal(p => p.Password);
        RuleFor(p=>p.BirthDate).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .InclusiveBetween(new DateTime(1900,01,01),DateTime.Now)
            .WithMessage("The birth date is incorrect"); 
    }
}