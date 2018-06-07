using CarFactory;

namespace CarFactoryService
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CarFactoryDbContext : DbContext
    {

        public CarFactoryDbContext()
            : base("name=CarFactoryDbContext")
        {
        }

        public virtual DbSet<Consumer> Consumers { get; set; }
        public virtual DbSet<Ingridient> Ingridients { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Commodity> Commodities { get; set; }
        public virtual DbSet<CommodityIngridient> CommodityIngridients { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<StorageIngridient> StorageIngridients { get; set; }
        public virtual DbSet<MessageInfo> MessageInfos { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception)
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
                throw;
            }
        }
    }
}