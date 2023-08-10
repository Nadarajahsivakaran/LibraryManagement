using Library.DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task Create(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred  {ex.Message}");
                throw;
            }
        }

        public async Task Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await Save();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred  {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<T>> GetAll(params string[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (includeProperties != null)
                {
                    foreach (var property in includeProperties)
                    {
                        query = query.Include(property);
                    }
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred  {ex.Message}");
                throw;
            }
        }

        public async Task<T> GetData(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _dbSet.Where(predicate);
                if (includeProperties != null)
                {
                    foreach (var property in includeProperties)
                    {
                        query = query.Include(property);
                    }
                }
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred  {ex.Message}");
                throw;
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred  {ex.Message}");
                throw;
            }
        }

        public async Task Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred  {ex.Message}");
            }
        }

        public async Task<bool> IsValueExit(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).AnyAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred  {ex.Message}");
                throw;
            }
        }


    }
}
