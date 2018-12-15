using System.Linq;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class ClientRepository : RepositoryBase<Client>
    {
        public override bool Exists(Client item, out Client foundItem)
        {
            var list = Session.CreateQuery($@"from Client o where o.Name = '{item.Name}'").List<Client>();
            if (list.Count != 0)
            {
                item.Id = list.First().Id;
                foundItem = item;
                return true;
            }

            foundItem = null;
            return false;
        }
    }
}