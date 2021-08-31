using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Shared.Configurations.MessageBusses;
using RabbitMQ.Shared.Services.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Shared.Services.Concerete
{
    public class RabbitMessageQueueService : IMessageQueueService
    {
        private IConfiguration _configuration;

        public RabbitMessageQueueService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void PublishToQueue<T>(T response, string queueName, string exchange, string routeKey)
        {
            using(var connection=RabbitMQConfiguration.CreateConnection(_configuration))
            using(var channel = connection.CreateModel())
            { 
               channel.QueueDeclare(queueName, false, false, false, null);

               var message = JsonConvert.SerializeObject(response);
               var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange,routeKey, false, null, body);
            }


            

           

           
        }

        public void ReceiveValueFromQueue<T>(string queueName)
        {

         

            using (var connection = RabbitMQConfiguration.CreateConnection(_configuration))
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var data = Encoding.UTF8.GetString(body.Span);

                    T value=JsonConvert.DeserializeObject<T>(data);

                    channel.BasicAck(ea.DeliveryTag, false);

                    
                    
                };


                channel.BasicConsume(queueName, true, "", false, false, null, consumer);

            }    
        }
        
    }
}
