using Microsoft.AspNet.OData.Builder;
using RabbitMQ.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.WebAPI.Configurations.OData
{
    public static class ODataEntityConfigurations
    {

        public static ODataConventionModelBuilder EntityConfiguration()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<Product>("Products");

            return builder;
        }
    }
}
