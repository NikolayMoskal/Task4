using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Booking
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Product Product { get; set; }

        public Booking()
        {
        }
    }
}