using AskQuestion.Business.Abstract;
using AskQuestion.Business.Concrete.Managers;
using AskQuestion.DataAccess.Abstract;
using AskQuestion.DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Business.DependencyResolvers.Microsoft
{
    public static class BusinessModule
    {

        public static IServiceCollection AddBusinessModule(this IServiceCollection services)
        {

            services.AddScoped<DbContext, AskQuestionContext>();
            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<IAskedQuestionDal, EfAskedQuestionDal>();
            services.AddScoped<IQuestionDal, EfQuestionDal>();
            services.AddScoped<IQuestionAnswerDal, EfQuestionAnswerDal>();
            services.AddScoped<IUserQuestionDal, EfUserQuestionDal>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IQuestionService, QuestionManager>();

            return services;
        }

    }
}
