using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.WebAPI.Configurations.Data.EfCore
{
    public class NorthwindContext:DbContext
    {

        public NorthwindContext()
        {

        }

        public DbSet<Product> Products { get; set; }

        public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
        {

        }

    }
}
