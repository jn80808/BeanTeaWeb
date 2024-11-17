using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderItem> orderItems, string buyEmail,  Address shipToAddress, DeliveryMethod deliveryMethod, decimal subtotal) 
        {
            this.BuyEmail = buyEmail;
            //this.OrderDate = orderDate; -- set date in UTC NOW
            this.ShipToAddress = shipToAddress;
            this.DeliveryMethod = deliveryMethod;
            this.OrderItems = orderItems;
            this.Subtotal = subtotal;
            //this.Status = status; -- set status to pending so no need
            //this.PaymentIntendId = paymentIntendId; -- currently none
   
        }
        
        public string BuyEmail { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public Address ShipToAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        public decimal Subtotal { get; set; }

        public OrderStatus Status { get; set; }

        public string PaymentIntendId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }



    }
}