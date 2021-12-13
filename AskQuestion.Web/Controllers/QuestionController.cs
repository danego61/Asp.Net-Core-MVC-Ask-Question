using AskQuestion.Business.Abstract;
using AskQuestion.Entities;
using AskQuestion.Entities.Concrete;
using AskQuestion.Web.Models;
using AskQuestion.Web.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AskQuestion.Web.Controllers
{

    public class QuestionController : Controller
    {

        private readonly IQuestionService _service;
        private readonly IDataProtector _protector;

        public QuestionController(IQuestionService service, IDataProtectionProvider protector)
        {
            _service = service;
            _protector = protector.CreateProtector("QuestionController");
        }

        public IActionResult QuestionStats(string id)
        {
            AskedQuestion question = _service.GetAskedQuestion(id, true);

            if (question.UserID != null)
            {
                ClaimsIdentity identity = (ClaimsIdentity)HttpContext.User.Identity;
                if (identity.IsAuthenticated && Convert.ToInt32(identity.FindFirst("UserID").Value) != question.UserID)
                    return Unauthorized();
            }
            if (question.NameSurname != null)
            {
                if (HttpContext.Request.Cookies.TryGetValue("question", out string securedUrl))
                {
                    if (!id.Equals(_protector.Unprotect(securedUrl)))
                        return Unauthorized();
                }
                else
                    return Unauthorized();
            }

            string[] questions = new string[question.Questions.Count];
            string[] userNames = new string[question.Questions.First().Answers.Count];
            string[,] answers = new string[userNames.Length, questions.Length];
            bool[,] isCorrect = new bool[userNames.Length, questions.Length];

            for (int i = 0; i < question.Questions.Count; i++)
            {
                UserQuestion q = question.Questions[i];
                questions[i] = q.Question.QuestionTitle;
                for (int ii = 0; ii < q.Answers.Count; ii++)
                {
                    QuestionAnswer a = q.Answers[ii];
                    answers[ii, i] = a.Answer switch
                    {
                        QuestionOptions.Option1 => q.Question.Option1,
                        QuestionOptions.Option2 => q.Question.Option2,
                        QuestionOptions.Option3 => q.Question.Option3,
                        QuestionOptions.Option4 => q.Question.Option4,
                        _ => q.Question.Option5
                    };
                    isCorrect[ii, i] = q.CorrectOption == a.Answer;
                    if (i == 0)
                        userNames[ii] = a.NameSurname;
                }
            }

            QuestionStatsViewModel model = new()
            {
                NameSurname = question.NameSurname,
                Questions = questions,
                Answers = answers,
                userNames = userNames,
                IsCorrect = isCorrect,
                Url = id
            };

            return View(model);
        }

        public IActionResult AnswerQuestion(string id)
        {
            AskedQuestion question = _service.GetAskedQuestion(id, true);
            if (question.UserID == null && question.Questions.First().Answers.Count >= 5)
                return View();

            return View(new AnswerQuestionViewModel
            {
                Name = question.UserID == null ? question.NameSurname : question.User.NameSurname,
                IsSignedIn = ((ClaimsIdentity)HttpContext.User.Identity).IsAuthenticated,
                Questions = question.Questions.Select(x => new QuestionModel
                {
                    QuestionID = x.Question.QuestionPoolID,
                    Question = x.Question.QuestionTitle,
                    Choices = new string[] { x.Question.Option1, x.Question.Option2, x.Question.Option3, x.Question.Option4, x.Question.Option5 },
                    CorrectChoice = (int)x.CorrectOption
                }).ToList()
            });
        }

        [HttpPost]
        public IActionResult AnswerQuestion(string id, QuestionsWithChoicesModel model)
        {
            try
            {
                List<QuestionOptions> choices = model.Choices.Select(x => (QuestionOptions)x).ToList();
                if (model.NameSurname == null)
                    _service.AnswerQuestion(id, model.Questions, choices);
                else
                    _service.AnswerQuestion(id, model.NameSurname, model.Questions, choices);
                return Ok();
            }
            catch (SecurityException)
            {
                return Json(new { message = "Please sign in or write name surname!" });
            }
            catch (ValidationException ex)
            {
                return Json(new { message = string.Join('\n', ex.Errors.Select(x => x.ErrorMessage).ToArray()) });
            }
        }

        public IActionResult AskQuestion()
        {
            bool isSignedIn = ((ClaimsIdentity)HttpContext.User.Identity).IsAuthenticated;
            List<Question> questions = isSignedIn ? _service.GetAllQuestions() : _service.GetFirstQuestions();
            AskQuestionViewModel model = new()
            {
                IsSignedIn = isSignedIn,
                Questions = questions.Select(x => new QuestionModel
                {
                    QuestionID = x.QuestionPoolID,
                    Question = x.QuestionTitle,
                    Choices = new string[] { x.Option1, x.Option2, x.Option3, x.Option4, x.Option5 },
                    CorrectChoice = null
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AskQuestion(QuestionsWithChoicesModel model)
        {
            try
            {
                UserQuestion[] questions = new UserQuestion[model.Questions.Count];
                for (int i = 0; i < questions.Length; i++)
                {
                    questions[i] = new UserQuestion
                    {
                        QuestionPoolID = model.Questions[i],
                        CorrectOption = (QuestionOptions)model.Choices[i]
                    };
                }
                AskedQuestion askedQuestion = model.NameSurname == null ?
                    _service.AskQuestion(questions) :
                    _service.AskQuestion(model.NameSurname, questions);
                if (model.NameSurname != null)
                {
                    CookieOptions options = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(3) };
                    HttpContext.Response.Cookies.Append("question", _protector.Protect(askedQuestion.QuestionUrl), options);
                }
                return Json(new { url = Url.Action(nameof(AskedQuestions)) });
            }
            catch (SecurityException)
            {
                return Json(new { message = "Please sign in or write name surname!" });
            }
            catch (ValidationException ex)
            {
                return Json(new { message = string.Join('\n', ex.Errors.Select(x => x.ErrorMessage).ToArray()) });
            }
        }

        public IActionResult AskedQuestions()
        {
            bool isSignedIn = ((ClaimsIdentity)HttpContext.User.Identity).IsAuthenticated;
            List<AskedQuestionsViewModel> model = new();
            if (isSignedIn)
            {
                HttpContext.Response.Cookies.Delete("question");
                model.AddRange(_service.GetUserAskedQuestions().Where(x => x.Questions.Count > 0).Select(x => new AskedQuestionsViewModel
                {
                    QuestionUrl = x.QuestionUrl,
                    QuestionCount = x.Questions.Count,
                    QuestionAnswersCount = x.Questions.First().Answers.Count
                }));
            }
            else
            {
                if (HttpContext.Request.Cookies.TryGetValue("question", out string securedUrl))
                {
                    string url = _protector.Unprotect(securedUrl);
                    AskedQuestion question = _service.GetAskedQuestion(url);
                    model.Add(new AskedQuestionsViewModel
                    {
                        QuestionUrl = question.QuestionUrl,
                        QuestionCount = question.Questions.Count,
                        QuestionAnswersCount = question.Questions.First().Answers.Count
                    });
                }
                else
                {
                    return RedirectToAction(nameof(LoginController.SignIn), nameof(LoginController).Replace("Controller", ""));
                }
            }
            return View(model.ToArray());
        }

        public IActionResult MyQuestions()
        {
            try
            {
                return View(new MyQuestionsViewModel
                {
                    IsAdmin = HttpContext.User.IsInRole("Admin"),
                    Questions = _service.GetUserQuestions().Select(x => new QuestionAndStatusModel
                    {
                        QuestionId = x.QuestionPoolID,
                        QuestionTitle = x.QuestionTitle,
                        Option1 = x.Option1,
                        Option2 = x.Option2,
                        Option3 = x.Option3,
                        Option4 = x.Option4,
                        Option5 = x.Option5,
                        Status = x.QuestionPoolStatus,
                        StatusText = x.QuestionPoolStatus switch
                        {
                            QuestionPoolStatus.WaitingForApproval => "Waiting For Approval",
                            QuestionPoolStatus.Approved => "Approved",
                            QuestionPoolStatus.Rejected => "Rejected",
                        }
                    }).ToList()
                });
            }
            catch (SecurityException)
            {
                return Unauthorized();
            }
        }

        public IActionResult AddQuestion(NewQuestionModel model)
        {
            _service.AddQuestion(model.QuestionTitle, model.Option1, model.Option2, model.Option3, model.Option4, model.Option5);
            return RedirectToAction(nameof(MyQuestions));
        }

        public IActionResult QuestionStatusUpdate(int questionId, bool accept)
        {
            _service.QuestionStatusUpdate(questionId, accept);
            return RedirectToAction(nameof(MyQuestions));
        }

        public IActionResult QuestionDelete(int questionId)
        {
            _service.QuestionDelete(questionId);
            return RedirectToAction(nameof(MyQuestions));
        }

    }

}
