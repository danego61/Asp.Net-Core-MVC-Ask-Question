using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AskQuestion.Core.Utils;
using AskQuestion.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AskQuestion.DataAccess.Concrete.EntityFramework.Mappings
{

    public class UserMap : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder
                .ToTable("Users");

            builder
                .HasKey(x => x.UserID)
                .HasName("UserID");

            builder
                .Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnType("nvarchar(25)");

            builder
                .Property(x => x.Surname)
                .HasColumnName("Surname")
                .IsRequired()
                .HasMaxLength(25)
                .HasColumnType("nvarchar(25)");

            builder
                .Property(x => x.Password)
                .HasColumnName("Password")
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnType("nchar(64)");

            builder
                .Property(x => x.Email)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder
               .HasIndex(x => x.Email)
               .IsUnique();

            builder
                .Property(x => x.IsAdmin)
                .HasColumnName("Admin")
                .IsRequired();

            builder
                .Property(x => x.EmailVerifyToken)
                .HasColumnName("EmailVerifyToken");

            builder
                .Property(x => x.IsEmailVerified)
                .HasColumnName("EmailVerified");

            builder
                .HasMany(x => x.UserAskedQuestions)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserID);

            builder
                .HasMany(x => x.UserQuestions)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserID);

            builder
                .Ignore(x => x.NameSurname);

            builder
                .HasData(
                    new User
                    {
                        Name = "Yusuf",
                        Surname = "Akbaş",
                        Email = "danego61@gmail.com",
                        Password = PasswordHashUtils.GetPasswordHash("Yusuf61."),
                        IsAdmin = true,
                        UserID = 1,
                        IsEmailVerified = true
                    }
                );

        }

    }

}
