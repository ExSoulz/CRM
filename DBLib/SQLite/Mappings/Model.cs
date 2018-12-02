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

        public override string ToString()
        {
            return $"{this.ID} {this.Name} {this.Brand.Name} {this.TechType.Name}";
        }

    }

    class ModelMap : ClassMap<Model>
    {
        public ModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Unique();
            References(x => x.Brand)
                .Cascade.All();
            References(x => x.TechType)
                .Cascade.All();
        }
    }
}
