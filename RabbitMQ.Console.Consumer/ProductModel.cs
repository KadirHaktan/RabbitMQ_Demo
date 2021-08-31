﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public class ProductModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public int SupplierID { get; set; }

        public int CategoryID { get; set; }

        public string QuantityPerUnit { get; set; }

        public Int16 UnitsInStock { get; set; }

        public Int16 UnitsOnOrder { get; set; }

        public Int16 ReorderLevel { get; set; }

        public bool Discontinued { get; set; }
    }
}