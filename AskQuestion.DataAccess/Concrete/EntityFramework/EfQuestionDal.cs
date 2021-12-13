using AskQuestion.Core.DataAccess.EntityFramework;
using AskQuestion.DataAccess.Abstract;
using AskQuestion.Entities;
using AskQuestion.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.DataAccess.Concrete.EntityFramework
{
    public class EfQuestionDal : EfEntityRepositoryBase<Question>, IQuestionDal
    {

        public EfQuestionDal(DbContext context) : base(context)
        {

        }

        public List<Question> GetFirstQuestions()
        {
            return _context.Set<Question>().Where(x => x.QuestionPoolStatus == QuestionPoolStatus.Approved).Take(10).ToList();
        }
    }
}
