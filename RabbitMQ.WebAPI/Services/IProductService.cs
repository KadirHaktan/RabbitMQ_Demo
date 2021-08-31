using RabbitMQ.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.WebAPI.Services
{
    public interface IProductService
    {

        List<Product> ListAllProducts();


        Product GetProduct(int id);
    }
}
