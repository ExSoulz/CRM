﻿using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;


namespace DBLib.SQLite.Mappings
{
    public class ServicePoint
    {
        public virtual int ID { get; set; }
        public virtual string Title { get; set; }
        public virtual string Adress { get; set; }
        public virtual ISet<Operator> Operators { get; set; }
        public virtual ISet<CRMApplication> Applications { get; set; }

        public override string ToString()
        {
            return $"{this.ID}     {this.Title}      {this.Adress}      {this.Operators.Count}      {this.Applications.Count}";
        }
    }

     class ServicePointMap : ClassMap<ServicePoint>
    {
        public ServicePointMap()
        {
            Id(x => x.ID);
            Map(x => x.Title);
            Map(x => x.Adress);
            HasMany(x => x.Operators)
                .Cascade.AllDeleteOrphan();
            HasMany(x => x.Applications)
                .Cascade.AllDeleteOrphan();
        }
    }
}
