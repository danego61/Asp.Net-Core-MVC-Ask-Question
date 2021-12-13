using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskQuestion.Web.Models
{
    public class QuestionModel
    {

        public int QuestionID { get; set; }

        public string Question { get; set; }

        public string[] Choices { get; set; }

        public int? CorrectChoice { get; set; }

    }
}
