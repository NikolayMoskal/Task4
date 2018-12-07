using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccessLayer.Entities.Mappings
{
    public class ProductMapping : ClassMapping<Product>
    {
        public ProductMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Name);
            Property(x => x.Date);
            Property(x => x.Price);
            ManyToOne(x => x.Order, c =>
            {
                c.Cascade(Cascade.Persist);
                c.Column("Order_Id");
            });
        }
    }
}