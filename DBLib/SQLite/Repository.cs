using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace DBLib
{
    public interface IRepository<T> : IDisposable
            where T : class
    {
        List<T> GetList();
        T GetEntity(int id);
        void Save(T entity);
        void Delete(int id);
        List<T> GetListByValue(string parameter,string criteria);
    }

    public class Repository<T> : IRepository<T>
        where T : class
    {

        private ISession session;

        public Repository(ISession _session)
        {
            session = _session;
        }

        public void Save(T entity)
        {
            session.Save(entity);
        }

        public void Delete(int id)
        {
            session.Delete(id);
        }

        public T GetEntity(int id)
        {
            return session.Get<T>(id);
        }

        public List<T> GetList()
        {
            return new List<T>(session.CreateCriteria(typeof(T)).List<T>());
        }

        public List<T> GetListByValue(string parameter, string criteria)
        {
            return session.CreateCriteria<T>()
                .Add(Expression.Like(parameter, criteria, MatchMode.Exact))
                .List<T>() as List<T>;
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
