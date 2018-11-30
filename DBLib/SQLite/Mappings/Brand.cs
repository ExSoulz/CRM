using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;


namespace DBLib.SQLite.Mappings
{
    public class Brand
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual ISet<Model> Models { get; set; }
    }

    class BrandMap : ClassMap<Brand>
    {
        public BrandMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Unique();
            HasMany(x => x.Models)
                .Cascade.SaveUpdate()
                .Inverse();
        }
    }
}
