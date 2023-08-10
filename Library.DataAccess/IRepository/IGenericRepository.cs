using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(params string[] includeProperties);

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<T> GetData(Expression<Func<T, bool>> predicate, params string[] includeProperties);

        Task Save();

        Task<bool> IsValueExit(Expression<Func<T, bool>> predicate);
    }
}
