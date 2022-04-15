using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    public class Response
    {
        public ResponseType Type
        {
            get;
            set;
        }

        public object Data { get; set; }

        private Response() { }

        public string toString()
        {
            return "Request{" +
                    "type='" + Type + '\'' +
                    ", data='" + Data + '\'' +
                    '}';
        }


        public class Builder
        {
            private Response response = new Response();

            public Builder type(ResponseType type)
            {
                response.Type = type;
                return this;
            }

            public Builder data(object data)
            {
                response.Data = data;
                return this;
            }

            public Response build()
            {
                return response;
            }
        }
    }
}
