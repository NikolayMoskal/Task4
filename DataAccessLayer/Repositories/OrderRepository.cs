using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public override bool Exists(Order item)
        {
            return Session.CreateQuery($@"from Order o where o.Id = '{item.Id}'").List().Count == 0;
        }
    }
}