using AskQuestion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Entities.Concrete
{
    public class Question : IEntity
    {

        public int QuestionPoolID { get; set; }

        public int UserID { get; set; }

        public string QuestionTitle { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        public string Option5 { get; set; }

        public QuestionPoolStatus QuestionPoolStatus { get; set; }

        public List<UserQuestion> UserQuestions { get; set; }

        public virtual User User { get; set; }

        public Question()
        {
            UserQuestions = new();
        }

    }
}
