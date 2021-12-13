using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AskQuestion.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nchar(64)", maxLength: 64, nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    EmailVerifyToken = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserID", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "AskedQuestions",
                columns: table => new
                {
                    AskedQuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionUrl = table.Column<string>(type: "nchar(6)", maxLength: 6, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    NameSurname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AskedQuestionID", x => x.AskedQuestionID);
                    table.ForeignKey(
                        name: "FK_AskedQuestions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionPool",
                columns: table => new
                {
                    QuestionPoolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    QuestionTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Option1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Option2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Option3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Option4 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Option5 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    QuestionPoolStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("QuestionPoolID", x => x.QuestionPoolID);
                    table.ForeignKey(
                        name: "FK_QuestionPool_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserQuestion",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AskedQuestionID = table.Column<int>(type: "int", nullable: false),
                    QuestionPoolID = table.Column<int>(type: "int", nullable: false),
                    CorrectOption = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("QuestionID", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_UserQuestion_AskedQuestions_AskedQuestionID",
                        column: x => x.AskedQuestionID,
                        principalTable: "AskedQuestions",
                        principalColumn: "AskedQuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserQuestion_QuestionPool_QuestionPoolID",
                        column: x => x.QuestionPoolID,
                        principalTable: "QuestionPool",
                        principalColumn: "QuestionPoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswer",
                columns: table => new
                {
                    QuestionAnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    NameSurname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Answer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("QuestionAnswerID", x => x.QuestionAnswerID);
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_UserQuestion_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "UserQuestion",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "EmailVerifyToken", "Admin", "EmailVerified", "Name", "Password", "Surname" },
                values: new object[] { 1, "danego61@gmail.com", null, true, true, "Yusuf", "143ae4caf57afa86ce1d34adee6273f67963c59ddaebb05bb104778d483aca25", "Akbaş" });

            migrationBuilder.InsertData(
                table: "QuestionPool",
                columns: new[] { "QuestionPoolID", "Option1", "Option2", "Option3", "Option4", "Option5", "QuestionPoolStatus", "QuestionTitle", "UserID" },
                values: new object[,]
                {
                    { 1, "Patates Kızartması", "Burger", "Döner", "Kuru Fasulye", "Makarna", 1, "En sevdiğim yemek?", 1 },
                    { 2, "Pop", "Rap", "Rock", "Türk Halk Müziği", "Arabesk", 1, "En sevdiğim müzik türü?", 1 },
                    { 3, "Uyuyarak", "Bilgisayar başında", "Yürüyüş yaparak", "Kitap okuyarak", "Arkadaşlarıyla buluşarak", 1, "Zamanımı nasıl geçiririm?", 1 },
                    { 4, "Kayıp parayı bulmak", "Tuttuğu takımın galibiyeti", "Süpriz hediye almak", "Alışveriş mağazasındaki indirimler", "Çekilişle telefon kazanmak", 1, "Beni en çok ne sevindirir?", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AskedQuestions_QuestionUrl",
                table: "AskedQuestions",
                column: "QuestionUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AskedQuestions_UserID",
                table: "AskedQuestions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_QuestionID",
                table: "QuestionAnswer",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPool_UserID",
                table: "QuestionPool",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestion_AskedQuestionID",
                table: "UserQuestion",
                column: "AskedQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestion_QuestionPoolID",
                table: "UserQuestion",
                column: "QuestionPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswer");

            migrationBuilder.DropTable(
                name: "UserQuestion");

            migrationBuilder.DropTable(
                name: "AskedQuestions");

            migrationBuilder.DropTable(
                name: "QuestionPool");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
