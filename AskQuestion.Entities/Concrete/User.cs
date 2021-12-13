using AskQuestion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Entities.Concrete
{
    public class User : IEntity
    {

        public int UserID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public Guid? EmailVerifyToken { get; set; }

        public bool IsEmailVerified { get; set; }

        public List<AskedQuestion> UserAskedQuestions { get; set; }

        public List<Question> UserQuestions { get; set; }

        public string NameSurname { get => $"{Name} {Surname}"; }

        public User()
        {
            UserAskedQuestions = new();
            UserQuestions = new();
        }

    }
}
