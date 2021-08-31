using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.WebAPI.Models
{
    public class PublisherModel<T>
    {
        public T data { get; set; }

        public string message { get; set; }
    }
}
