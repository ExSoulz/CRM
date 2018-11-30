using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using DBLib.SQLite.Mappings;
using System;

namespace DBLib
{
    public class NhibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString("Data Source=CRMdb.db;Version=3;")
                )
                //.ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CRMApplication>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }

    }
}
