using AskQuestion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Entities.Concrete
{
    public class AskedQuestion : IEntity
    {

        public int AskedQuestionID { get; set; }

        public string QuestionUrl { get; set; }

        public int? UserID { get; set; }

        public string NameSurname { get; set; }

        public List<UserQuestion> Questions { get; set; }

        public virtual User User { get; set; }

        public AskedQuestion()
        {
            Questions = new();
        }

    }
}
