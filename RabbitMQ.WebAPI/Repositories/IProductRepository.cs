using RabbitMQ.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.WebAPI.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAll();

        Product GetById(int id);
    }
}
