using AskQuestion.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AskQuestion.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {

        public UserValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Username must not be a empty.")
                .MaximumLength(25)
                .WithMessage("Username must be a maximum of 25 characters.")
                .MinimumLength(2)
                .WithMessage("Username must be a minimum 2 characters.");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Surname must not be a empty.")
                .MaximumLength(25)
                .WithMessage("Surname must be a maximum of 25 characters.")
                .MinimumLength(2)
                .WithMessage("Surname must be a minimum 2 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-mail must not be a empty.")
                .EmailAddress()
                .WithMessage("Enter the e-mail address in the correct format.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password must not be a empty.");

        }

    }
}
