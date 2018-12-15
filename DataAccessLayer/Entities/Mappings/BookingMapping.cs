using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace DataAccessLayer.Entities.Mappings
{
    public class BookingMapping : ClassMapping<Booking>
    {
        public BookingMapping()
        {
            Id(x => x.Id, map => map.Generator(Generators.Native));
            Property(x => x.Date);
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
            ManyToOne(x => x.Product, c =>
            {
                c.Cascade(Cascade.Persist);
                c.Column("Product_Id");
            });
        }
    }
}