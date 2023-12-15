using System.Linq.Expressions;

namespace GrandeTravel.Data
{
    public interface IRepository<T>
    {
        void Create(T entity);
        IEnumerable<T> GetAll();
        T GetSingle(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Delete(T entity);
    }
}
