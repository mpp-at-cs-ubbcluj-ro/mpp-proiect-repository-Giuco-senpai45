using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP.model;



namespace MPP.repository
{
    internal class AbstractRepo<T,Tid> : Repository<T, Tid> where T : Identifiable<Tid>
    {

        private Dictionary<Tid, T> _elems;
        public void add(T entity)
        {
            if (_elems.ContainsKey(entity.Id))
            {
                throw new Exception("Element already exists");
            }
            else
                _elems.Add(entity.Id, entity);
            return
        }

        public void delete(T entity)
        {
            if (_elems.ContainsKey(entity.Id))
            {
                _elems.Remove(entity.Id);
            }
        }

        public ICollection<T> findAll()
        {
            return _elems.Values.ToList();
        }

        public T findbyId(Tid id)
        {
            if (_elems.ContainsKey(id))
            {
                return _elems[id];
            }
            else
            {
                throw new Exception("Element doesn't exist");
            }
        }

        public void update(T entity, Tid id)
        {
            if (_elems.ContainsKey(entity.Id))
            {
                _elems.Add(entity.Id, entity);
            }
            else
            {
                throw new Exception("Element doesn't exist");
            }

        }
    }
}
