using AskQuestion.Business.Abstract;
using AskQuestion.Business.ValidationRules.FluentValidation;
using AskQuestion.Core.Aspects.Postsharp.AuthorizationAspects;
using AskQuestion.Core.Aspects.Postsharp.ValidationAspects;
using AskQuestion.DataAccess.Abstract;
using AskQuestion.Entities;
using AskQuestion.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AskQuestion.Business.Concrete.Managers
{

    public class QuestionManager : IQuestionService
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private readonly IQuestionDal _questionDal;
        private readonly IAskedQuestionDal _askedQuestionDal;

        public QuestionManager(IQuestionDal questionDal, IAskedQuestionDal askedQuestionDal)
        {
            _questionDal = questionDal;
            _askedQuestionDal = askedQuestionDal;
        }

        [SignedInOperation]
        public void AddQuestion(string title, string option1, string option2, string option3, string option4, string option5)
        {
            _questionDal.Add(new Question
            {
                QuestionTitle = title,
                Option1 = option1,
                Option2 = option2,
                Option3 = option3,
                Option4 = option4,
                Option5 = option5,
                QuestionPoolStatus = Thread.CurrentPrincipal.IsInRole("Admin") ? QuestionPoolStatus.Approved : QuestionPoolStatus.WaitingForApproval,
                UserID = int.Parse(((ClaimsIdentity)Thread.CurrentPrincipal.Identity).FindFirst("UserID").Value)
            });
            _questionDal.SaveChanges();
        }

        public void AnswerQuestion(string questionUrl, string nameSurname, List<int> questions, List<QuestionOptions> answers)
        {
            AskedQuestion question = GetAskedQuestion(questionUrl, true);
            for (int i = 0; i < question.Questions.Count; i++)
            {
                question.Questions[i].Answers.Add(new QuestionAnswer
                {
                    NameSurname = nameSurname,
                    QuestionID = questions[i],
                    Answer = answers[i]
                });
            }
            _askedQuestionDal.Update(question);
            _askedQuestionDal.SaveChanges();
        }

        [SignedInOperation]
        public void AnswerQuestion(string questionUrl, List<int> questions, List<QuestionOptions> answers)
        {
            AskedQuestion question = GetAskedQuestion(questionUrl, true);
            for (int i = 0; i < question.Questions.Count; i++)
            {
                question.Questions[i].Answers.Add(new QuestionAnswer
                {
                    NameSurname = ((ClaimsIdentity)Thread.CurrentPrincipal.Identity).FindFirst(ClaimTypes.Name).Value,
                    QuestionID = questions[i],
                    Answer = answers[i]
                });
            }
            _askedQuestionDal.Update(question);
            _askedQuestionDal.SaveChanges();
        }

        [SignedInOperation]
        public AskedQuestion AskQuestion(UserQuestion[] questions)
        {
            return AskQuestion(new AskedQuestion
            {
                Questions = questions.ToList(),
                UserID = Convert.ToInt32(((ClaimsIdentity)Thread.CurrentPrincipal.Identity).FindFirst("UserID").Value)
            });
        }

        public AskedQuestion AskQuestion(string userName, UserQuestion[] questions)
        {
            return AskQuestion(new AskedQuestion
            {
                Questions = questions.ToList(),
                NameSurname = userName
            });
        }

        public List<Question> GetAllQuestions()
        {
            return _questionDal.GetList(x => x.QuestionPoolStatus == QuestionPoolStatus.Approved);
        }

        public List<Question> GetFirstQuestions()
        {
            return _questionDal.GetFirstQuestions();
        }

        [SignedInOperation]
        public AskedQuestion[] GetUserAskedQuestions()
        {
            return _askedQuestionDal.GetQuestions(Convert.ToInt32(((ClaimsIdentity)Thread.CurrentPrincipal.Identity).FindFirst("UserID").Value));
        }

        public AskedQuestion GetAskedQuestion(string askedQuestionUrl, bool withQuestion = false)
        {
            if (withQuestion)
                return _askedQuestionDal.GetQuestionwithQuestion(askedQuestionUrl);

            return _askedQuestionDal.GetQuestion(askedQuestionUrl);
        }

        [SignedInOperation]
        public Question[] GetUserQuestions()
        {
            if (Thread.CurrentPrincipal.IsInRole("Admin"))
                return _questionDal.GetList(x => x.QuestionPoolStatus != QuestionPoolStatus.Rejected).ToArray();
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            return _questionDal.GetList(x => x.UserID == int.Parse(claimsIdentity.FindFirst("UserID").Value)).ToArray();
        }

        [FluentValidationAspect(typeof(AskedQuestionValidator))]
        private AskedQuestion AskQuestion(AskedQuestion question)
        {
            question.QuestionUrl = new string(
                Enumerable.Repeat(Chars, 6).Select(x => Chars[RandomNumberGenerator.GetInt32(Chars.Length)]).ToArray()
                );
            _askedQuestionDal.Add(question);
            _askedQuestionDal.SaveChanges();
            return question;
        }

        [SignedInOperation]
        public void QuestionStatusUpdate(int questionId, bool accept)
        {
            if (!Thread.CurrentPrincipal.IsInRole("Admin"))
                throw new SecurityException();
            Question question = _questionDal.Get(x => x.QuestionPoolID == questionId);
            if (question.QuestionPoolStatus == QuestionPoolStatus.WaitingForApproval)
            {
                question.QuestionPoolStatus = accept ? QuestionPoolStatus.Approved : QuestionPoolStatus.Rejected;
                _questionDal.Update(question);
                _questionDal.SaveChanges();
            }
        }

        [SignedInOperation]
        public void QuestionDelete(int questionId)
        {
            if (!Thread.CurrentPrincipal.IsInRole("Admin"))
                throw new SecurityException();
            _questionDal.Delete(_questionDal.Get(x => x.QuestionPoolID == questionId));
            _questionDal.SaveChanges();
        }

    }

}
