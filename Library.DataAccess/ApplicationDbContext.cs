using Library.Models;
using Library.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }

        public DbSet<Book> Book { get; set; }

        public DbSet<IssuingBooks> IssuingBooks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    IdNumber = "S001",
                    FirstName = "Siva",
                    LastName = "Karan",
                    Email = "karan@gmail.com",
                    Address = "Jaffna",
                    PhoneNumber = "0773601787",
                    RegisteredDate = DateTime.Now,
                    TerminatedDate = DateTime.Now,
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    BookReference = "A001",
                    ISBN = "SLH",
                    Title = "Histroy",
                    Author = "Mala",
                    Publication = "SLBR",
                    Edition = "TUIJ",
                    PublishedYear = "2020",
                    Category = BookCategory.Fiction,
                    NoOfCopies = 5

                }
            );

            modelBuilder.Entity<IssuingBooks>().HasData(
                new IssuingBooks
                {
                    IssuingBookId = 1,
                    BookId = 1,
                    StudentId = 1,
                    IssueDate = DateTime.Now,
                    ReturnDate = DateTime.Now,
                    NoOfCopies = 2
                }
            );


            base.OnModelCreating(modelBuilder);
        }
    }


}