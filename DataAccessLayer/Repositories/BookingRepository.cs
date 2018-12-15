using System.Linq;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class BookingRepository : RepositoryBase<Booking>
    {
        public override bool Exists(Booking item, out Booking foundItem)
        {
            var list = Session.CreateQuery($@"from Booking o where o.Date = '{item.Date:yyyy-MM-dd}'" + 
                                           $@"and o.Client.Name = '{item.Client.Name}'" +
                                           $@"and o.Employee.Name = '{item.Employee.Name}'" +
                                           $@"and o.Product.Name = '{item.Product.Name}'").List<Booking>();
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