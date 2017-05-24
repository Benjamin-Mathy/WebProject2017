using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject2017Server.Modèle;

namespace WebProject2017Server.EF.Mappers
{
    public class EFJourneyMapper : EFMapper<Journey>
    {
        public EFJourneyMapper(string connectionString): base(connectionString) { }

        public new List<Journey> AddorUpdate(List<Journey> entitys)
        {
            try
            {
                context.Journeys.Load();
                context.Users.Load();
                context.Addresses.Load();
                foreach (Journey entity in entitys)
                {
                    if (entity != null && entity != new Journey())
                    {
                        if (entity.Driver != null)
                        {
                            if (null != context.Users.Where(u => u.Login == entity.Driver.Login))
                            {
                                entity.Driver = context.Users.Where(u => u.Login == entity.Driver.Login).First();
                            }
                        }
                        if (entity.StartingAddress != null)
                        {
                            if (null != context.Addresses.Find(entity.StartingAddress.ID))
                            {
                                entity.StartingAddress = context.Addresses.Find(entity.StartingAddress.ID);
                            }
                        }
                        if (entity.DriverUpAddress != null)
                        {
                            if (null != context.Addresses.Find(entity.DriverUpAddress.ID))
                            {
                                entity.DriverUpAddress = context.Addresses.Find(entity.DriverUpAddress.ID);
                            }
                        }
                        if (context.Journeys.Find(entity.ID) == null)
                        {
                            context.Journeys.Add(entity);
                        }
                        else
                        {
                            context.Entry<Journey>(entity).CurrentValues.SetValues(entity);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("entity empty");
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
        public new Journey AddorUpdate(Journey entity)
        {
            try
            {
                if (entity != null && entity != new Journey())
                {
                    context.Journeys.Load();
                    context.Users.Load();
                    context.Addresses.Load();
                    if (entity.Driver != null)
                    {
                        if (null != context.Users.Where(u => u.Login == entity.Driver.Login))
                        {
                            entity.Driver = context.Users.Where(u => u.Login == entity.Driver.Login).First();
                        }
                    }
                    if(entity.Passengers != null && entity.Passengers.Count > 0)
                    {
                        for(int i= 0;i < entity.Passengers.Count;i++)
                        {
                            if (null != context.Users.Where(u => u.Login == entity.Passengers[i].Login))
                            {
                                entity.Passengers[i] = context.Users.Where(u => u.Login == entity.Passengers[i].Login).First();
                            }
                        }
                    }
                    if (entity.StartingAddress != null)
                    {
                        if (null != context.Addresses.Find(entity.StartingAddress.ID))
                        {
                            entity.StartingAddress = context.Addresses.Find(entity.StartingAddress.ID);
                        }
                    }
                    if (entity.DriverUpAddress != null)
                    {
                        if (null != context.Addresses.Find(entity.DriverUpAddress.ID))
                        {
                            entity.DriverUpAddress = context.Addresses.Find(entity.DriverUpAddress.ID);
                        }
                    }
                    if (context.Set<Journey>().Find(entity.ID) == null)
                    {
                        context.Set<Journey>().Add(entity);
                    }
                    else
                    {
                        context.Entry<Journey>(entity).CurrentValues.SetValues(entity);
                    }
                }
                else
                {
                    throw new ArgumentException("entity empty");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            context.SaveChanges();
            return entity;
        }
    }
}
