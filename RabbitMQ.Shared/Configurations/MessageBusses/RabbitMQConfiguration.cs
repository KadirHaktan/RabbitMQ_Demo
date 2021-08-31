using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Shared.Configurations.MessageBusses
{
    public class RabbitMQConfiguration
    {
        public static IConnection CreateConnection(IConfiguration configuration)
        {
            var uri = new ConnectionFactory
            {
                Uri = new Uri(configuration["Configurations:RabbitMQUri"])
            };


            return uri.CreateConnection();
        }
    }
}
