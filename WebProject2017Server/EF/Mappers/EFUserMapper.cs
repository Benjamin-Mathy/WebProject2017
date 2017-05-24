using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject2017Server.Modèle;

namespace WebProject2017Server.EF.Mappers
{
    public class EFUserMapper : EFMapper<User>
    {
        public EFUserMapper(string connectionString): base(connectionString) { }

        public new List<User> AddorUpdate(List<User> entitys)
        {
            try
            {                
                context.Users.Load();
                context.Addresses.Load();
                foreach (User entity in entitys)
                {
                    if (entity != null && entity != new User())
                    {
                        if (loginAlreadyExisting(entity) || EmailAlreadyExisting(entity))
                        {
                            return entitys;
                        }                  
                        if (entity.Address !=null && null != context.Addresses.Find(entity.Address.ID))
                        {
                            entity.Address = context.Addresses.Find(entity.Address.ID);
                        }
                        if (context.Set<User>().Find(entity.ID) == null)
                        {
                            context.Set<User>().Add(entity);
                        }
                        else
                        {
                            context.Entry<User>(entity).CurrentValues.SetValues(entity);
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new DbEntityValidationException("entity empty");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            return entitys;
        }
        public new User AddorUpdate(User entity)
        {
            try
            {
                if (entity != null && entity != new User())
                {
                    if (loginAlreadyExisting(entity) || EmailAlreadyExisting(entity))
                    {
                        return entity;
                    }
                    context.Users.Load();
                    context.Addresses.Load();
                    if (entity.Address != null && null != context.Addresses.Find(entity.Address.ID))
                    {
                        entity.Address = context.Addresses.Find(entity.Address.ID);
                    }
                    if (context.Set<User>().Find(entity.ID) == null)
                    {
                        context.Set<User>().Add(entity);
                    }
                    else
                    {
                        context.Entry<User>(entity).CurrentValues.SetValues(entity);
                    }
                }
                else
                {
                    throw new DbEntityValidationException("entity empty");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            context.SaveChanges();
            return entity;
        }
        public Boolean loginAlreadyExisting(User entity)
        {
            try
            {
                context.Users.Load();
                List<User> list = context.Users.Where(u => u.Login == entity.Login).ToList<User>();
                if (list.Count == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }
        public Boolean EmailAlreadyExisting(User entity)
        {
            try
            {
                context.Users.Load();
                List<User> list = context.Users.Where(u => u.Email == entity.Email).ToList<User>();
                if (list.Count == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }
    }
}
