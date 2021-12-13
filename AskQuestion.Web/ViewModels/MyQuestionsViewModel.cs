using AskQuestion.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskQuestion.Web.ViewModels
{
    public class MyQuestionsViewModel
    {

        public bool IsAdmin { get; set; }

        public List<QuestionAndStatusModel> Questions { get; set; }

    }
}
