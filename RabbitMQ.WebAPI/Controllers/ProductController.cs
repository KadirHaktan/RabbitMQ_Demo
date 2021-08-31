using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Shared.Configurations.MessageBusses;
using RabbitMQ.Shared.Services.Abstract;
using RabbitMQ.WebAPI.Entities;
using RabbitMQ.WebAPI.Models;
using RabbitMQ.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private IMessageQueueService _messageQueueService;

        public ProductController(IProductService service,IMessageQueueService messageQueueService)
        {
            this._service = service;
            this._messageQueueService = messageQueueService;
        }



        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _service.ListAllProducts();

            _messageQueueService.PublishToQueue(response, "products", "", "products");

            return Ok(new PublisherModel<List<Product>>
            {
                data = response,
                message = "Channel send to all products to queue"
            });
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var response = _service.GetProduct(id);


            _messageQueueService.PublishToQueue(response, $"{response.ProductName}", "", $"{response.ProductName}");


            return Ok(new PublisherModel<Product>
            {
                data = response,
                message = $"Channel send to {response.ProductName} to queue"
            });
        }
    }
}
