using AskQuestion.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Business.ValidationRules.FluentValidation
{
    public class UserQuestionValidator : AbstractValidator<UserQuestion>
    {

        public UserQuestionValidator()
        {

            RuleFor(x => x.QuestionPoolID)
                .NotEqual(0).WithMessage("Question not be empty!");

        }

    }
}
