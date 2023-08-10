using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookReference = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Publication = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Edition = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PublishedYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    NoOfCopies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "IssuingBooks",
                columns: table => new
                {
                    IssuingBookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoOfCopies = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuingBooks", x => x.IssuingBookId);
                    table.ForeignKey(
                        name: "FK_IssuingBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssuingBooks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "BookReference", "Category", "Edition", "ISBN", "NoOfCopies", "Publication", "PublishedYear", "Title" },
                values: new object[] { 1, "Mala", "A001", 1, "TUIJ", "SLH", 5, "SLBR", "2020", "Histroy" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Address", "Email", "FirstName", "IdNumber", "LastName", "PhoneNumber", "RegisteredDate", "TerminatedDate" },
                values: new object[] { 1, "Jaffna", "karan@gmail.com", "Siva", "S001", "Karan", "0773601787", new DateTime(2023, 8, 8, 21, 45, 21, 131, DateTimeKind.Local).AddTicks(2736), new DateTime(2023, 8, 8, 21, 45, 21, 131, DateTimeKind.Local).AddTicks(2737) });

            migrationBuilder.InsertData(
                table: "IssuingBooks",
                columns: new[] { "IssuingBookId", "BookId", "IssueDate", "NoOfCopies", "ReturnDate", "StudentId" },
                values: new object[] { 1, 1, new DateTime(2023, 8, 8, 21, 45, 21, 131, DateTimeKind.Local).AddTicks(2933), 2, new DateTime(2023, 8, 8, 21, 45, 21, 131, DateTimeKind.Local).AddTicks(2934), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_IssuingBooks_BookId",
                table: "IssuingBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_IssuingBooks_StudentId",
                table: "IssuingBooks",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssuingBooks");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
