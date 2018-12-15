using System.Linq;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        public override bool Exists(Employee item, out Employee foundItem)
        {
            var list = Session.CreateQuery($@"from Employee o where o.Name = '{item.Name}'").List<Employee>();
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