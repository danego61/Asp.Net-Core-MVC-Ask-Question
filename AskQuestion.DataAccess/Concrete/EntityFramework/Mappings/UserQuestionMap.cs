using AskQuestion.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserQuestionMap : IEntityTypeConfiguration<UserQuestion>
    {

        public void Configure(EntityTypeBuilder<UserQuestion> builder)
        {

            builder
                .ToTable("UserQuestion");

            builder
                .HasKey(x => x.QuestionID)
                .HasName("QuestionID");

            builder
                .Property(x => x.CorrectOption)
                .HasColumnName("CorrectOption")
                .IsRequired()
                .HasConversion<int>();

            builder
                .HasOne(x => x.AskedQuestion)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.AskedQuestionID);

            builder
                .HasOne(x => x.Question)
                .WithMany(x => x.UserQuestions)
                .HasForeignKey(x => x.QuestionPoolID);

            builder
                .HasMany(x => x.Answers)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionID);

        }

    }
}
