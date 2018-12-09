using System;

namespace BusinessLayer.Transfers
{
    public class ProductTransfer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public OrderTransfer Order { get; set; }
    }
}