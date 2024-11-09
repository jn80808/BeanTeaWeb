using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, int productName, int imageUrl) 
        {
            this.ProductItemId = productItemId;
            this.ProductName = productName;
            this.imageUrl = imageUrl;
   
        }
        public int ProductItemId { get; set; }

        public int ProductName { get; set; }

        public int imageUrl { get; set; }

        
        
    }
}