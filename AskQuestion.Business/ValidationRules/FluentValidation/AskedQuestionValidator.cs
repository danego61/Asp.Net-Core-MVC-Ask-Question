using AskQuestion.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Business.ValidationRules.FluentValidation
{

    public class AskedQuestionValidator : AbstractValidator<AskedQuestion>
    {

        public AskedQuestionValidator()
        {
            RuleFor(x => x.NameSurname).NotEmpty().When(x => x.UserID == null).WithMessage("Please sign in or write name surname!");
            RuleFor(x => x.Questions)
                .Must(x => x.Count > 0).WithMessage("Questions not be empty!")
                .Must(x => x.Count <= 10).WithMessage("Please select up to 10 questions with answers.")
                .Must(x => x.Count <= 4).When(x => x.UserID == null).WithMessage("Please select up to 4 questions with answers!");
            RuleForEach(x => x.Questions).SetValidator(new UserQuestionValidator());
        }

    }

}
