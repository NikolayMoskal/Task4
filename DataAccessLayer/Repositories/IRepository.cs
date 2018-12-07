using System.Collections.Generic;

namespace DataAccessLayer.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetOne(int id);
        void Save(T item);
        void Delete(T item);
        void DeleteAll();
    }
}