using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.IRepository
{
    public interface IIsueBookRepository : IGenericRepository<IssuingBooks>
    {
        Task<bool> IsBookAvilable(int bookId, int count);
    }
}
