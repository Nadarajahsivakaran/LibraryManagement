using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.IRepository
{
    public interface IUnitOfWork
    {
        public IStudentRepository Student { get; }

        public IBookRepository Book { get; }

        public IIsueBookRepository IssueBook { get; }
    }
}
