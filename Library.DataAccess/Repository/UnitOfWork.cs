using Library.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IStudentRepository Student { get; private set; }

        public IBookRepository Book { get; private set; }

        public IIsueBookRepository IssueBook { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext) {
            Student = new StudentRepository(dbContext);
            Book = new BookRepository(dbContext);
            IssueBook = new IsueBookRepository(dbContext);
        }
    }
}
