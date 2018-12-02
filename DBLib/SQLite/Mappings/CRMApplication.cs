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
        public virtual string TypeName { get; set; }
        public virtual TechType Type { get ; set; }
        public virtual string BrandName { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual string ModelName { get; set; }
        public virtual Model Model { get; set; }
        public virtual string Status { get; set; }

        public override string ToString()
        {
            return $"{this.ID}   {this.Applicator}    {AcceptingDate.ToString()}   {this.Operator.FullName}    {this.ServicePoint.Title}    {this.PhoneNumber}    {this.Reason}    {this.Type.Name}    {this.Brand.Name}    {this.Model.Name}    {this.Status}";
        }

        public CRMApplication()
        {
            Status = "Обработан";
        }
    }

    class CRMApplicationMap : ClassMap<CRMApplication>
    {
        public CRMApplicationMap()
        {
            Id(x => x.ID);
            Map(x => x.Applicator);
            Map(x => x.AcceptingDate);
            Map(x => x.PhoneNumber);
            Map(x => x.Reason);
            Map(x => x.Status);
            References(x => x.ServicePoint)
                .Cascade.All();
            References(x => x.Operator)
                .Fetch.Join()
                .Cascade.All();
            References(x => x.Brand)
                .Fetch.Join()
                .Cascade.All();
            References(x => x.Type)
                .Fetch.Join()
                .Cascade.All();
            References(x => x.Model)
                .Fetch.Join()
                .Cascade.All();

        }
    }

}
