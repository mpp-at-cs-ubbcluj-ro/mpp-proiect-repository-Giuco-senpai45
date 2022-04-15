using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace networking
{
    [Serializable]
    public class Request
    {
        public RequestType Type
        {
            get;
            set;
        }

        public object Data { get; set; }

        private Request() { }

        public string toString()
        {
            return "Request{" +
                    "type='" + Type + '\'' +
                    ", data='" + Data + '\'' +
                    '}';
        }


        public class Builder
        {
            private Request request = new Request();

            public Builder type(RequestType type)
            {
                request.Type = type;
                return this;
            }

            public Builder data(object data)
            {
                request.Data = data;
                return this;
            }

            public Request build()
            {
                return request;
            }
        }
    }
}
