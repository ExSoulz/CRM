using System;
using FluentNHibernate.Mapping;

namespace DBLib.SQLite.Mappings
{
    public class CRMApplication
    {
        public virtual int ID { get; set; }
        public virtual string Applicator { get; set; }
        public virtual DateTime AcceptingDate { get; set; }
        public virtual Operator Operator { get; set; }
        public virtual ServicePoint ServicePoint { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Reason { get; set; }
        public virtual TechType Type { get ; set; }
        public virtual Brand Brand { get; set; }
        public virtual Model Model { get; set; }
    }

    class CRMApplicationMap : ClassMap<CRMApplication>
    {
        public CRMApplicationMap()
        {
            Id(x => x.ID);
            Map(x => x.Applicator);
            Map(x => x.AcceptingDate);
            Map(x => x.PhoneNumber);
            References(x => x.ServicePoint)
                .Cascade.SaveUpdate();
            References(x => x.Operator)
                .Fetch.Join()
                .Cascade.SaveUpdate();
            References(x => x.Brand)
                .Fetch.Join()
                .Cascade.SaveUpdate();
            References(x => x.Type)
                .Fetch.Join()
                .Cascade.SaveUpdate();
            References(x => x.Model)
                .Fetch.Join()
                .Cascade.SaveUpdate();
        }
    }

}
