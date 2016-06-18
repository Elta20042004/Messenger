using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class Response<T>
    {
        public T Data { get; set; }

        public ResponseCode ErrorCode { get; set; }

        public Response(T data, ResponseCode errorCode)
        {
            Data = data;
            ErrorCode = errorCode;
        }

        public Response()
        {
        }
    }
}
