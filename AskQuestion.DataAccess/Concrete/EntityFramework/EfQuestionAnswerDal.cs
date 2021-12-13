using AskQuestion.Core.DataAccess.EntityFramework;
using AskQuestion.DataAccess.Abstract;
using AskQuestion.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.DataAccess.Concrete.EntityFramework
{
    public class EfQuestionAnswerDal : EfEntityRepositoryBase<QuestionAnswer>, IQuestionAnswerDal
    {

        public EfQuestionAnswerDal(DbContext context) : base(context)
        {

        }

    }
}
