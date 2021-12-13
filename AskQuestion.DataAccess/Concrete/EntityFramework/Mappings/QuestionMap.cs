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
    public class QuestionMap : IEntityTypeConfiguration<Question>
    {

        public void Configure(EntityTypeBuilder<Question> builder)
        {

            builder
                .ToTable("QuestionPool");

            builder
                .HasKey(x => x.QuestionPoolID)
                .HasName("QuestionPoolID");

            builder
                .Property(x => x.QuestionTitle)
                .HasColumnName("QuestionTitle")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder
                .Property(x => x.Option1)
                .HasColumnName("Option1")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder
               .Property(x => x.Option2)
               .HasColumnName("Option2")
               .IsRequired()
               .HasMaxLength(50)
               .HasColumnType("nvarchar(50)");

            builder
               .Property(x => x.Option3)
               .HasColumnName("Option3")
               .IsRequired()
               .HasMaxLength(50)
               .HasColumnType("nvarchar(50)");

            builder
               .Property(x => x.Option4)
               .HasColumnName("Option4")
               .IsRequired()
               .HasMaxLength(50)
               .HasColumnType("nvarchar(50)");

            builder
               .Property(x => x.Option5)
               .HasColumnName("Option5")
               .IsRequired()
               .HasMaxLength(50)
               .HasColumnType("nvarchar(50)");

            builder
                .Property(x => x.QuestionPoolStatus)
                .HasColumnName("QuestionPoolStatus")
                .IsRequired()
                .HasConversion<int>();

            builder
                .HasMany(x => x.UserQuestions)
                .WithOne(x => x.Question)
                .HasForeignKey(x => x.QuestionID);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserQuestions)
                .HasForeignKey(x => x.UserID);

            builder
                .HasData(
                    new Question
                    {
                        QuestionPoolID = 1,
                        QuestionTitle = "En sevdiğim yemek?",
                        Option1 = "Patates Kızartması",
                        Option2 = "Burger",
                        Option3 = "Döner",
                        Option4 = "Kuru Fasulye",
                        Option5 = "Makarna",
                        UserID = 1,
                        QuestionPoolStatus = Entities.QuestionPoolStatus.Approved
                    },
                    new Question
                    {
                        QuestionPoolID = 2,
                        QuestionTitle = "En sevdiğim müzik türü?",
                        Option1 = "Pop",
                        Option2 = "Rap",
                        Option3 = "Rock",
                        Option4 = "Türk Halk Müziği",
                        Option5 = "Arabesk",
                        UserID = 1,
                        QuestionPoolStatus = Entities.QuestionPoolStatus.Approved
                    },
                    new Question
                    {
                        QuestionPoolID = 3,
                        QuestionTitle = "Zamanımı nasıl geçiririm?",
                        Option1 = "Uyuyarak",
                        Option2 = "Bilgisayar başında",
                        Option3 = "Yürüyüş yaparak",
                        Option4 = "Kitap okuyarak",
                        Option5 = "Arkadaşlarıyla buluşarak",
                        UserID = 1,
                        QuestionPoolStatus = Entities.QuestionPoolStatus.Approved
                    },
                    new Question
                    {
                        QuestionPoolID = 4,
                        QuestionTitle = "Beni en çok ne sevindirir?",
                        Option1 = "Kayıp parayı bulmak",
                        Option2 = "Tuttuğu takımın galibiyeti",
                        Option3 = "Süpriz hediye almak",
                        Option4 = "Alışveriş mağazasındaki indirimler",
                        Option5 = "Çekilişle telefon kazanmak",
                        UserID = 1,
                        QuestionPoolStatus = Entities.QuestionPoolStatus.Approved
                    }
                );

        }

    }
}
