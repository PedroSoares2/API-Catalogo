using System.Linq.Expressions;

namespace APICatalogo.Repository.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T> All();
    T? Get(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}
