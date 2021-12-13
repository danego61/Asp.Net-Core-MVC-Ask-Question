using AskQuestion.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskQuestion.Web.ViewModels
{
    public class AnswerQuestionViewModel
    {

        public string Name { get; set; }

        public bool IsSignedIn { get; set; }

        public List<QuestionModel> Questions { get; set; }

        public AnswerQuestionViewModel()
        {
            Questions = new();
        }

    }
}
