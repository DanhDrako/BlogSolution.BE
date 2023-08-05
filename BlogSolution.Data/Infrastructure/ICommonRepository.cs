using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSolution.Data.Infrastructure
{
    public interface ICommonRepository<T> where T : class
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        Task<T> Delete(int id);
        Task<T> GetSingleById(Guid id);
        Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);
        Task<List<T>> GetAll(string[] includes = null);
        Task<List<T>> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);
        Task<List<T>> GetMultiPaging(Expression<Func<T, bool>> predicate, int total, int index = 0, int size = 50, string[] includes = null);

    }
}
