using System;
using System.Collections.Generic;
using DataAccessLayer.Configurations;
using NHibernate;

namespace DataAccessLayer.Repositories
{
    public class RepositoryBase<T> : IDisposable, IRepository<T> where T : class
    {
        protected ISession Session;
        protected ITransaction Transaction;
        
        public RepositoryBase()
        {
            Session = NHibernateConfiguration.OpenSession();
            Transaction = null;
        }

        public RepositoryBase(ISession session)
        {
            Session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public void BeginTransaction()
        {
            Transaction = Session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Transaction.Commit();
            CloseTransaction();
        }

        public void RollbackTransaction()
        {
            Transaction.Rollback();
            CloseTransaction();
            CloseSession();
        }

        private void CloseTransaction()
        {
            Transaction.Dispose();
            Transaction = null;
        }

        private void CloseSession()
        {
            Session.Close();
            Session.Dispose();
            Session = null;
        }

        public IEnumerable<T> GetAll()
        {
            return Session.CreateQuery($"from {typeof(T)}").List<T>();
        }

        public T GetOne(int id)
        {
            return Session.Load<T>(id);
        }

        public void Save(T item)
        {
            Session.SaveOrUpdate(item);
        }

        public void Delete(T item)
        {
            Session.Delete(item);
        }

        public void DeleteAll()
        {
            Session.CreateQuery($"delete {typeof(T)} t").ExecuteUpdate();
        }

        public void Dispose()
        {
            if (Transaction != null)
            {
                CommitTransaction();
                //RollbackTransaction();
            }

            if (Session != null)
            {
                Session.Flush();
                CloseSession();
            }
        }
    }
}