using AskQuestion.Core.DataAccess;
using AskQuestion.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.DataAccess.Abstract
{
    public interface IAskedQuestionDal : IEntityRepository<AskedQuestion>
    {

        AskedQuestion GetQuestionwithQuestion(string url);

        AskedQuestion GetQuestion(string url);

        AskedQuestion[] GetQuestions(int userId);

    }
}
