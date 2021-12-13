using AskQuestion.Entities.Concrete;
using AskQuestion.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskQuestion.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AskQuestion.DataAccess.Concrete.EntityFramework
{
    public class EfAskedQuestionDal : EfEntityRepositoryBase<AskedQuestion>, IAskedQuestionDal
    {

        public EfAskedQuestionDal(DbContext context) : base(context)
        {

        }

        public AskedQuestion GetQuestionwithQuestion(string url)
        {
            return _context.Set<AskedQuestion>()
                .Where(x => x.QuestionUrl == url)
                .Include(x => x.User)
                .Include(x => x.Questions).ThenInclude(x => x.Question)
                .Include(x => x.Questions).ThenInclude(x => x.Answers)
                .SingleOrDefault();
        }

        public AskedQuestion GetQuestion(string url)
        {
            return _context.Set<AskedQuestion>()
                .Where(x => x.QuestionUrl == url)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .SingleOrDefault();
        }

        public AskedQuestion[] GetQuestions(int userId)
        {
            return _context.Set<AskedQuestion>()
                .Where(x => x.UserID == userId)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .ToArray();
        }
    }
}
