using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.OrderAggregate
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string firtName, string lastName,string street,
                       string city, string state, string zipCode) 
        {
            this.FirtName = firtName;
            this.LastName = lastName;
            this.Street = street;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
   
        }
        public string FirtName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }
    }
}