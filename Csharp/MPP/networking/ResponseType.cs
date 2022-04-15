using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    public enum ResponseType
    {
        OK,
        ERROR,
        UPDATE,
        NEW_MATCH_LIST,
        ORG_LOGGED_IN,
        ORG_LOGGED_OUT,
        SOLD_TICKET,
        GOT_MATCHES
    }
}
