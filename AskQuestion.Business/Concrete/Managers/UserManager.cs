using AskQuestion.Business.Abstract;
using AskQuestion.Business.ValidationRules.FluentValidation;
using AskQuestion.Core.Aspects.Postsharp.ValidationAspects;
using AskQuestion.Core.CrossCuttingConcerns.Email;
using AskQuestion.Core.Utils;
using AskQuestion.DataAccess.Abstract;
using AskQuestion.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AskQuestion.Business.Concrete.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _UserDal;

        public UserManager(IUserDal userDal)
        {
            _UserDal = userDal;
        }

        [FluentValidationAspect(typeof(UserValidator))]
        public User AddUser(User user)
        {
            user.Password = PasswordHashUtils.GetPasswordHash(user.Password);
            User us = _UserDal.Add(user);
            _UserDal.SaveChanges();
            SendVerifyEmail(us);
            return us;
        }

        public User GetUser(int userId)
        {
            return _UserDal.Get(x => x.UserID == userId);
        }

        public User GetUser(string userEmail)
        {
            return _UserDal.Get(x => x.Email == userEmail);
        }

        public bool VerifyUserEmail(Guid token)
        {
            User user = _UserDal.Get(x => x.EmailVerifyToken == token);
            if (user == null)
                return false;
            user.EmailVerifyToken = null;
            user.IsEmailVerified = true;
            _UserDal.Update(user);
            _UserDal.SaveChanges();
            return true;
        }

        public void SendVerifyEmail(User user)
        {
            user.EmailVerifyToken = Guid.NewGuid();
            SendEmail.Send(user.Email, "Ask Question Account Verification", $"<a href='http://localhost:5000/Login/Verify/{user.EmailVerifyToken}'>Click here</a><span> and verify account!</span>");
            _UserDal.Update(user);
            _UserDal.SaveChanges();
        }

    }
}
