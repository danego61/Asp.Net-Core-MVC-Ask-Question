using AskQuestion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskQuestion.Web.Models
{
    public class QuestionAndStatusModel
    {

        public int QuestionId { get; set; }

        public string QuestionTitle { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        public string Option5 { get; set; }

        public QuestionPoolStatus Status { get; set; }

        public string StatusText { get; set; }

    }
}
