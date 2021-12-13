using AskQuestion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Entities.Concrete
{
    public class QuestionAnswer : IEntity
    {

        public int QuestionAnswerID { get; set; }

        public int QuestionID { get; set; }

        public string NameSurname { get; set; }

        public QuestionOptions Answer { get; set; }

        public UserQuestion Question { get; set; }

    }
}
