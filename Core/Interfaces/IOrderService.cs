using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Core.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order>CreateOrderAsync(string buyerEmail, int delivery, string basketId,
        OrderAggregate.Address shippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

        Task<Order> GetOrderByIdAsync(int id, string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();




    }
}