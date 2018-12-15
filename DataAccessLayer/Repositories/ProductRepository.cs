using System.Linq;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public override bool Exists(Product item, out Product foundItem)
        {
            var list = Session.CreateQuery($@"from Product o where o.Name = '{item.Name}'").List<Product>();
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