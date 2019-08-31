using System.Collections.Generic;

namespace Library.Database.Contracts
{
    public interface IDatabase<T>
    {
        void Create(T item);
        List<T> Read();
        void Update();
        void Delete(T item);
        T Find(int id);
        T Find(string name);
    }
}
