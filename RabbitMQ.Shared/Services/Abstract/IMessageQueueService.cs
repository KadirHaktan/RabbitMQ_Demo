using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Shared.Services.Abstract
{
    public interface IMessageQueueService
    {
        void PublishToQueue<T>(T response, string queueName, string exchange, string routeKey);

        void ReceiveValueFromQueue<T>(string queueName);
    }
}
