using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.repository
{
    internal interface Repository<T, Tid>
    {
        void add(T entity);
        void delete(T entity);
        void update(T entity,Tid id);
        T findbyId(Tid id);

        ICollection<T> findAll();
    }
}
