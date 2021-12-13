using AskQuestion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Entities.Concrete
{
    public class UserQuestion : IEntity
    {

        public int QuestionID { get; set; }

        public int AskedQuestionID { get; set; }

        public int QuestionPoolID { get; set; }

        public QuestionOptions CorrectOption { get; set; }

        public virtual Question Question { get; set; }

        public virtual AskedQuestion AskedQuestion { get; set; }

        public List<QuestionAnswer> Answers { get; set; }

        public UserQuestion()
        {
            Answers = new();
        }

    }
}
