using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskQuestion.Web.ViewModels
{
    public class UserSignUpViewModel
    {

        public string UserEmail { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserPassword { get; set; }

        public string ReUserPassword { get; set; }

    }
}
