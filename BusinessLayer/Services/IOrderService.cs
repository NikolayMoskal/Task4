using System.Collections.Generic;
using BusinessLayer.Transfers;

namespace BusinessLayer.Services
{
    public interface IOrderService
    {
        void MakeNewOrder(OrderTransfer order);
        EmployeeTransfer GetEmployee(int id);
        ClientTransfer GetClient(int id);
        IEnumerable<ProductTransfer> GetProducts(int orderId);
    }
}