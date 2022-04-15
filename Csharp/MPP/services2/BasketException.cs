using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services2
{
    public class BasketException : Exception
    {
        public BasketException() : base() { }

        public BasketException(string msg) : base(msg) { }

        public BasketException(string msg, Exception ex) : base(msg, ex) { }

    }
}

