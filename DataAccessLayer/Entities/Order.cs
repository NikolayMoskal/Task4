using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}