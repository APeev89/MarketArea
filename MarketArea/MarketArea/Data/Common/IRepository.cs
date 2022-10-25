using MarketArea.Data.ModelDb;
using System.Linq;
using System.Linq.Expressions;

namespace MarketArea.Data.Common
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        T GetById<T>(object id) where T : class;

        IQueryable<T> All<T>() where T : class;

        IQueryable<T> All<T>(Expression<Func<T, bool>> search) where T : class;
        int SaveChanges();
    }
}
