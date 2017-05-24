using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
                            context.Users.AddOrUpdate(entity);
                        }
                        try
                        {
                            context.SaveChanges();
                        }
                        catch(Exception ex)
                        {
                            if (loginAlreadyExisting(entity))
                            {
                                throw new ArgumentException("Ce login (" + entity.Login+ ")  est déjà utiliser pour un autre compte", ex);
                            }
                            else if (EmailAlreadyExisting(entity))
                            {
                                throw new ArgumentException("Cette adresse email ("+entity.Email+") est déjà utiliser pour un autre compte", ex);
                            }
                        }
                    }
                    else
                    {
                        throw new DbEntityValidationException("entity empty");
                    }
                }
            }
            catch (Exception ex)
            {                
                throw new DbEntityValidationException(ex.Message, ex);
            }
            return entitys;
        }
        public new User AddorUpdate(User entity)
        {
            try
            {
                if (entity != null && entity != new User())
                {
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
                        context.Users.AddOrUpdate(entity);
                    }
                }
                else
                {
                    throw new DbEntityValidationException("entity empty");
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (loginAlreadyExisting(entity))
                {
                    throw new ArgumentException("Ce login (" + entity.Login + ")  est déjà utiliser pour un autre compte", ex);
                }
                else if (EmailAlreadyExisting(entity))
                {
                    throw new ArgumentException("Cette adresse email (" + entity.Email + ") est déjà utiliser pour un autre compte", ex);
                }
                throw new DbEntityValidationException(ex.Message, ex);
            }            
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
