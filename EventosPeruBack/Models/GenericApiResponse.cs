using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventosPeruBack.Models
{
    [Serializable]
    public class GenericApiResponse<T>
    {
        public int status { get; set; }
        public string msg { get; set; }

        public T data { get; set; }

        public GenericApiResponse()
        {
            this.status = 200;
            this.msg = "OK";
        }
    }
}