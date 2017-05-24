using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace WebProject2017Server.EF.Mappers
{
    public class EFMapper<T> : IDisposable,IMapper<T> where T : WithID
    {
        private readonly string CONNECTION_STRING;
        public DB_Context context { get; set; }

        public EFMapper(string connectionString)
        {
            this.CONNECTION_STRING = connectionString;
            OpenSession();
        }
        public List<T> GetAll()
        {
            try {
                context.Set<T>().Load();
                List<T> list = context.Set<T>().ToList<T>();
                
                return list;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public List<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            try
            {
                context.Set<T>().Load();
                List<T> list = context.Set<T>().Where(predicate).ToList<T>(); ;
                return list;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }
        public T FindBy(int id)
        {
            try
            {
                context.Set<T>().Load();
                List<T> list = context.Set<T>().Where(e => e.ID == id).ToList<T>();
                if (list.Count==0)
                {
                    return null;
                }
                return list.First();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public List<T> AddorUpdate(List<T> entitys)
        {
            try {
                context.Set<T>().Load();
                foreach (T entity in entitys)
                {
                    if (context.Set<T>().Find(entity.ID)==null){
                        context.Set<T>().Add(entity);
                    }
                    else
                    {
                        context.Entry<T>(entity).CurrentValues.SetValues(entity);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {   
                throw new ArgumentException(ex.Message, ex);
            }
            return entitys;
        }
        public T AddorUpdate(T entity)
        {
            try
            {
                context.Set<T>().Load();
                if (context.Set<T>().Find(entity.ID) == null)
                {
                    context.Set<T>().Add(entity);
                }
                else
                {
                    context.Entry<T>(entity).CurrentValues.SetValues(entity);
                }
                
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            context.SaveChanges();
            return entity;
        }

        public void Delete(int ID)
        {
            try {
                context.Set<T>().Load();
                context.Set<T>().Remove(FindBy(ID));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            context.SaveChanges();
        }
        public void Save()
        {
            if (context.Database.CurrentTransaction != null)
            {
                context.Database.CurrentTransaction.Commit();
            }
            context.SaveChanges();
        }
        public void OpenSession()
        {
            if (context != null)
            {
                Dispose();
            }
            context = new DB_Context(CONNECTION_STRING);
        }
        /*public void OpenSessionWithTransaction()
        {
            if (context != null)
            {
                Close();
            }
            context = new DB_Context(CONNECTION_STRING);
            
            context.Database.BeginTransaction();
        }*/
        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }
    }
}

// Source : http://www.tugberkugurlu.com/archive/generic-repository-pattern-entity-framework-asp-net-mvc-and-unit-testing-triangle
