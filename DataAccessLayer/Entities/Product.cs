using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual double Price { get; set; }
        public virtual IList<Booking> Bookings { get; set; }

        public Product()
        {
        }
    }
}