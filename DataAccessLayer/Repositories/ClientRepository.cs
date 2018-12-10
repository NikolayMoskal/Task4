using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class ClientRepository : RepositoryBase<Client>
    {
        public override bool Exists(Client item)
        {
            return Session.CreateQuery($@"from Client o where o.Name = '{item.Name}'").List().Count == 0;
        }
    }
}