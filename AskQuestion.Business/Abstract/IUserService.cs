using AskQuestion.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Business.Abstract
{
    public interface IUserService
    {

        User AddUser(User user);

        User GetUser(int userId);

        User GetUser(string userEmail);

        bool VerifyUserEmail(Guid token);

        void SendVerifyEmail(User userId);

    }
}
