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
    public class AskedQuestionMap : IEntityTypeConfiguration<AskedQuestion>
    {

        public void Configure(EntityTypeBuilder<AskedQuestion> builder)
        {

            builder
                .ToTable("AskedQuestions");

            builder
                .HasKey(x => x.AskedQuestionID)
                .HasName("AskedQuestionID");

            builder
                .Property(x => x.QuestionUrl)
                .HasColumnName("QuestionUrl")
                .IsRequired()
                .HasMaxLength(6)
                .HasColumnType("nchar(6)");

            builder
                .HasIndex(x => x.QuestionUrl)
                .IsUnique();

            builder
                .Property(x => x.NameSurname)
                .HasColumnName("NameSurname")
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserAskedQuestions)
                .HasForeignKey(x => x.UserID);

            builder
               .HasMany(x => x.Questions)
               .WithOne(x => x.AskedQuestion)
               .HasForeignKey(x => x.AskedQuestionID);

        }

    }
}
