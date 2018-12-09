using System.Collections.Generic;

namespace BusinessLayer.Transfers
{
    public class ClientTransfer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<OrderTransfer> Orders { get; set; }
    }
}