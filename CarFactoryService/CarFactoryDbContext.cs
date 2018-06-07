using CarFactory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryService
{
	[Table("CarFactoryDatabase")]
	public class CarFactoryDbContext : DbContext
	{
		public CarFactoryDbContext()
		{
			//настройки конфигурации для entity
			Configuration.ProxyCreationEnabled = false;
			Configuration.LazyLoadingEnabled = false;
			var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
		}

		public virtual DbSet<Consumer> Consumers { get; set; }

		public virtual DbSet<Ingridient> Ingridients { get; set; }

		public virtual DbSet<Worker> Workers { get; set; }

		public virtual DbSet<Booking> Bookings { get; set; }

		public virtual DbSet<Commodity> Commodities { get; set; }

		public virtual DbSet<CommodityIngridient> CommodityIngridients { get; set; }

		public virtual DbSet<Storage> Storages { get; set; }

		public virtual DbSet<StorageIngridient> StorageIngridients { get; set; }

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
