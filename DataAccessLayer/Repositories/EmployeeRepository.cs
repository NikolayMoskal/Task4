using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>
    {
        public override bool Exists(Employee item)
        {
            return Session.CreateQuery($@"from Employee o where o.Name = '{item.Name}'").List().Count == 0;
        }
    }
}