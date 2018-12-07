using System;

namespace DataAccessLayer.Entities
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual double Price { get; set; }
        public virtual Order Order { get; set; }
    }
}