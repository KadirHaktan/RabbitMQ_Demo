using RabbitMQ.WebAPI.Entities;
using RabbitMQ.WebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.WebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            this._repository = repository;
        }


        public Product GetProduct(int id)
        {
            return _repository.GetById(id);
        }

        public List<Product> ListAllProducts()
        {
            return _repository.GetAll();
        }
    }
}
