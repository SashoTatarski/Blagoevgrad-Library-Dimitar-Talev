using System.Collections.Generic;

namespace Library.Database.JsonHandler
{
    public interface IJsonHandler<T>
    {
        List<T> Load();
        void Save(List<T> collection);
    }
}
