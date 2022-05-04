using CV19.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CV19.Services.Interfaces
{
    internal interface IRepository<T> where T : IEntity
    {
        T Get(int id) => GetAll().FirstOrDefault(item => item.Id == id);

        IEnumerable<T> GetAll();

        void Add(T item);

        void Update(int id, T item);

        bool Remove(T item);
    }
}
