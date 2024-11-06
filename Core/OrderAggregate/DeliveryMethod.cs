using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.OrderAggregate
{
    public class DeliveryMethod : BaseEntity
    {
        public int ShortName { get; set; }

        public int DeliveryTime { get; set; }

        public int Description { get; set; }

        public int Price { get; set; }




    }
}