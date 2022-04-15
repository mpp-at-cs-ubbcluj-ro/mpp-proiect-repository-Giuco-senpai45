using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.model
{
    public interface Identifiable<Tid>
    {
        Tid Id { get; set; }
    }
}
