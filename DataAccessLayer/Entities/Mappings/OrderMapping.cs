using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccessLayer.Entities.Mappings
{
    public class OrderMapping : ClassMapping<Order>
    {
        public OrderMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            ManyToOne(x => x.Client, c =>
            {
                c.Cascade(Cascade.Persist);
                c.Column("Client_Id");
            });
            ManyToOne(x => x.Employee, c =>
            {
                c.Cascade(Cascade.Persist);
                c.Column("Employee_Id");
            });
            Bag(x => x.Products, c =>
            {
                c.Key(y => y.Column("Product_Id"));
                c.Inverse(true);
            }, r => r.OneToMany());
        }
    }
}