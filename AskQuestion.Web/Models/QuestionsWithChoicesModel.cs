using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskQuestion.Web.Models
{
    public class QuestionsWithChoicesModel
    {

        public string NameSurname { get; set; }

        public List<int> Questions { get; set; }

        public List<int> Choices { get; set; }

    }
}
