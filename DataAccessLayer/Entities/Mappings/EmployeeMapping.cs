using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccessLayer.Entities.Mappings
{
    public class EmployeeMapping : ClassMapping<Employee>
    {
        public EmployeeMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            Bag(x => x.Orders, c =>
            {
                c.Key(y => y.Column("Order_Id"));
                c.Inverse(true);
            }, r => r.OneToMany());
        }
    }
}