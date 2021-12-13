using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskQuestion.Web.ViewModels
{
    public class UserSignInViewModel
    {

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public bool RememberMe { get; set; }

    }
}
