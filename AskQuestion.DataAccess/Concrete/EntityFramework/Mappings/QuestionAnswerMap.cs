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
    public class QuestionAnswerMap : IEntityTypeConfiguration<QuestionAnswer>
    {

        public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
        {

            builder
                .ToTable("QuestionAnswer");

            builder
                .HasKey(x => x.QuestionAnswerID)
                .HasName("QuestionAnswerID");

            builder
                .Property(x => x.NameSurname)
                .HasColumnName("NameSurname")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder
                .Property(x => x.Answer)
                .HasColumnName("Answer")
                .IsRequired()
                .HasConversion<int>();

            builder
                .HasOne(x => x.Question)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.QuestionID);

        }

    }
}
