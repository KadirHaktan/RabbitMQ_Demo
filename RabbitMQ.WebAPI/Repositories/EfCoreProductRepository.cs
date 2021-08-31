using Microsoft.EntityFrameworkCore;
using RabbitMQ.WebAPI.Configurations.Data.EfCore;
using RabbitMQ.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.WebAPI.Repositories
{
    public class EfCoreProductRepository: IProductRepository
        
    {
        private readonly NorthwindContext _context;
        private DbSet<Product> _dbSet;

        public EfCoreProductRepository(NorthwindContext context)
        {
            this._context = context;
            _dbSet = context.Set<Product>();
        }
        public List<Product> GetAll()
        {
            return _dbSet.ToList();
        }

        public Product GetById(int id)
        {
            return _dbSet.Where(x => x.ProductID == id).FirstOrDefault();
        }
    }
}
