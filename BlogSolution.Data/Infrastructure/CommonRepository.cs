using BlogSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.Data.Infrastructure
{
    public class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        protected DataContext _context;
        private readonly DbSet<T> _dbSet;

        public CommonRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetSingleById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return  query.FirstOrDefault(expression);
            }   
            return _dbSet.FirstOrDefault(expression);
        }

        public async Task<List<T>> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return await query.ToListAsync();
            }

            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return await query.Where<T>(predicate).ToListAsync<T>();
            }

            return await _dbSet.Where<T>(predicate).ToListAsync<T>();
        }

        public async Task<List<T>> GetMultiPaging(Expression<Func<T, bool>> predicate, int total, int index = 0, int size = 50, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = _dbSet.Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? _dbSet.Where<T>(predicate).AsQueryable() : _dbSet.AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return await _resetSet.ToListAsync();
        }
    }
}
