using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public override bool Exists(Product item)
        {
            return Session.CreateQuery($@"from Product o where o.Name = '{item.Name}'").List().Count == 0;
        }
    }
}