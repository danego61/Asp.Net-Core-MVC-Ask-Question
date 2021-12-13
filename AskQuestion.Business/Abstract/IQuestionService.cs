using AskQuestion.Entities;
using AskQuestion.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Business.Abstract
{

    public interface IQuestionService
    {

        AskedQuestion AskQuestion(UserQuestion[] questions);

        AskedQuestion AskQuestion(string userName, UserQuestion[] questions);

        AskedQuestion[] GetUserAskedQuestions();

        AskedQuestion GetAskedQuestion(string askedQuestionUrl, bool withQuestion = false);

        void AnswerQuestion(string questionUrl, string nameSurname, List<int> questions, List<QuestionOptions> answers);

        void AnswerQuestion(string questionUrl, List<int> questions, List<QuestionOptions> answers);

        List<Question> GetFirstQuestions();

        List<Question> GetAllQuestions();

        void AddQuestion(string title, string option1, string option2, string option3, string option4, string option5);

        Question[] GetUserQuestions();

        void QuestionStatusUpdate(int questionId, bool accept);

        void QuestionDelete(int questionId);

    }

}
