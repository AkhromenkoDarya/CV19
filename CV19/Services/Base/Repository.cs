using System;
using CV19.Models.Interfaces;
using CV19.Services.Interfaces;
using System.Collections.Generic;

namespace CV19.Services.Base
{
    abstract class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly List<T> _items = new List<T>();

        private int _lastId;
        
        protected Repository()
        {
        }

        protected Repository(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        public IEnumerable<T> GetAll() => _items;

        public void Add(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (_items.Contains(item))
            {
                return;
            }

            item.Id = ++_lastId;
            _items.Add(item);
        }

        public void Update(int id, T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), id, 
                    "The item index cannot be less than 0.");
            }

            if (_items.Contains(item))
            {
                return;
            }

            T dbItem = ((IRepository<T>)this).Get(id);

            if (dbItem is null)
            {
                throw new InvalidOperationException("The item being edited not found " 
                                                    + "in the repository.");
            }

            Update(item, dbItem);
        }

        protected abstract void Update(T source, T destination);

        public bool Remove(T item) => _items.Remove(item);
    }
}
