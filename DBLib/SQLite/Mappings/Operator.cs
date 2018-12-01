using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace DBLib.SQLite.Mappings
{
    public class Operator
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string SecondName { get; set; }
        public virtual string FullName { get { return $"{Name} {SecondName}"; } }
        public virtual ServicePoint ServicePoint { get; set; }
        public virtual ISet<CRMApplication> Applications { get; set; }
    }

    class OpeatorMap : ClassMap<Operator>
    {
        public OpeatorMap()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.SecondName);
            HasMany(x => x.Applications)
                .Cascade.SaveUpdate();
            References(x => x.ServicePoint)
                .Fetch.Join()
                .Cascade.SaveUpdate();
        }
    }
}
