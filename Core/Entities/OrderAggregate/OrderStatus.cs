using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value="Pending")]
        Pending,

        [EnumMember(Value="PaymentRecevied")]
        PaymentRecevied,

        [EnumMember(Value="PaymentFailed")]
        PaymentFailed
    }
}