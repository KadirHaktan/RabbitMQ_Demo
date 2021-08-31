using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RabbitMQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Queue Name: ");
            string queueName = Console.ReadLine();


            ReceiveDataFromQueue(queueName);


        }


       
            

        private static void ReturnToListDataFromQueue(List<ProductModel> products)
        {

            foreach(var product in products)
            {
                ReturnToDataFromQueue(product);
                
            }
        }

        private static void ReturnToDataFromQueue(ProductModel product)
        {
            Console.WriteLine($"{product.ProductID}-{product.ProductName}-{product.UnitsInStock}-{product.QuantityPerUnit}");
        }


        private static void ReceiveDataFromQueue(string queueName)
        {
            var uri = new ConnectionFactory
            {
                Uri = new Uri("")
            };

            using (var connection = uri.CreateConnection())
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


                    if (queueName == "products")
                    {
                        List<ProductModel> products = JsonConvert.DeserializeObject<List<ProductModel>>(data);
                        ReturnToListDataFromQueue(products);
                    }

                    else
                    {
                        ProductModel product = JsonConvert.DeserializeObject<ProductModel>(data);
                        ReturnToDataFromQueue(product);
                    }



                };


                channel.BasicConsume(queueName, true, consumer);
               
                Console.ReadLine();


            }


        }
    }
}
