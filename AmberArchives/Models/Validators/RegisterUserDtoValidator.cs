using AmberArchives.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
		public RegisterUserDtoValidator(AmberArchivesDbContext dbContext)
		{
			RuleFor(x => x.Email).NotEmpty().EmailAddress();			

			RuleFor(x => x.Password).MinimumLength(6);

			RuleFor(x => x.PrimaryLanguage).NotEmpty();

			RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

			RuleFor(x => x.Email).Custom((value, context) =>
			{
				var emailInUse = dbContext.Users.Any(u => u.Email == value);
				if (emailInUse)
				{
					context.AddFailure("Email", "That email is taken");
				}
			});
			RuleFor(x => x.Username).NotEmpty().Custom((value, context) =>
			{
				var usernameInUse = dbContext.Users.Any(u => u.Username == value);
				if (usernameInUse)
				{
					context.AddFailure("Username", "That username is taken");
				}
			});
				
		}
    }
}
