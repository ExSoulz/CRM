using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace DBLib.SQLite.Mappings
{
    public class TechType
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual ISet<Model> Models { get; set; }

    }

    class TechTypeMap : ClassMap<TechType>
    {
        public TechTypeMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Unique();
            HasMany(x => x.Models)
                .Cascade.All()
                .Inverse();
        }
    }
}
