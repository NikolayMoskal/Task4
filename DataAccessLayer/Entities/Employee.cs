using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Booking> Bookings { get; set; }

        public Employee()
        {
        }
    }
}