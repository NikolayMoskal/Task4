using System.Collections.Generic;

namespace BusinessLayer.Transfers
{
    public class OrderTransfer
    {
        public int Id { get; set; }
        public ClientTransfer Client { get; set; }
        public EmployeeTransfer Employee { get; set; }
        public IList<ProductTransfer> Products { get; set; }
    }
}