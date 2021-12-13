using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskQuestion.Web.ViewModels
{
    public class QuestionStatsViewModel
    {

        public string NameSurname { get; set; }

        public string Url { get; set; }

        public string[] Questions { get; set; }

        public string[] userNames { get; set; }

        public string[,] Answers { get; set; }

        public bool[,] IsCorrect { get; set; }

    }
}
