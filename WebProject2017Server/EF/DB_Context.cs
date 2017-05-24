using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebProject2017Server.Modèle;

namespace WebProject2017Server.EF
{
    public class DB_Context : DbContext
    {
        public DB_Context(string connectionString) : base (connectionString) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Journey> Journeys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<DB_Context>(null);
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();*/
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Address>().HasKey<long>(a => a.ID);
            modelBuilder.Entity<Address>().Property(a => a.Country).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.Locality).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.Number).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.PostalCode).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.Road).IsRequired();           


            modelBuilder.Entity<User>().HasKey<long>(u => u.ID);
            modelBuilder.Entity<User>().Property(u => u.Login).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Phone).IsOptional();
            modelBuilder.Entity<User>().HasOptional<Address>(u => u.Address).WithMany(u => u.Users).WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Journey>().HasKey<long>(j => j.ID);
            modelBuilder.Entity<Journey>().Property(j => j.Description).IsRequired();
            modelBuilder.Entity<Journey>().Property(j => j.StartingDateTime).IsRequired();
            modelBuilder.Entity<Journey>().Property(j => j.DriverUpDatetime).IsRequired();
            modelBuilder.Entity<Journey>().HasRequired<Address>(j => j.DriverUpAddress).WithMany(a => a.JourneysDriverUpAddress).WillCascadeOnDelete(false);
            modelBuilder.Entity<Journey>().HasRequired<Address>(j => j.StartingAddress).WithMany(a => a.JourneysStaterAddress).WillCascadeOnDelete(false);
            modelBuilder.Entity<Journey>().HasRequired<User>(j => j.Driver).WithMany(u => u.DriverJourneys).WillCascadeOnDelete(false);
            modelBuilder.Entity<Journey>().HasMany<User>(j => j.Passengers).WithMany(u => u.UserJourneys)
                .Map(ju => {
                    ju.MapLeftKey("JourneyRefId");
                    ju.MapRightKey("UserRefId");
                    ju.ToTable("Passenger");
            });
        }
    }
}
