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
    public class EfUserDal : EfEntityRepositoryBase<User>, IUserDal
    {

        public EfUserDal(DbContext context) : base(context)
        {

        }

    }
}
