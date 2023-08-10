using Library.DataAccess.IRepository;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository
{
    public class IsueBookRepository : GenericRepository<IssuingBooks>, IIsueBookRepository
    {
        private readonly ApplicationDbContext dbContext;

        public IsueBookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> IsBookAvilable(int bookId, int count)
        {
            try
            {
                var today = DateTime.Today;
                var book = await dbContext.Book.FirstOrDefaultAsync(b=>b.BookId ==bookId);

                var outBooks = await dbContext.IssuingBooks.Where(i => i.BookId == bookId 
                && i.IssueDate<today&& i.ReturnDate>today).SumAsync(n=>n.NoOfCopies);

                return (book.NoOfCopies - outBooks) >= count;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
