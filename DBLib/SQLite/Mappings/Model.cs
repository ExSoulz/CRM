using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace DBLib.SQLite.Mappings
{
    public class Model
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual TechType TechType { get; set; }
    }

    class ModelMap : ClassMap<Model>
    {
        public ModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Unique();
            HasOne(x => x.Brand)
                .Cascade.SaveUpdate();
            HasOne(x => x.TechType)
                .Cascade.SaveUpdate();
        }
    }
}
