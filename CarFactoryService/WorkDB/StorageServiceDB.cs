using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryService.WorkDB
{
	public class StorageServiceDB : IStorage
	{
		private CarFactoryWebDbContext context;

		public StorageServiceDB(CarFactoryWebDbContext context)
		{
			this.context = context;
		}

        public StorageServiceDB()
        {
            this.context = new CarFactoryWebDbContext();
        }
        
        public List<StorageView> GetList()
		{
			List<StorageView> result = context.Storages
				.Select(rec => new StorageView
				{
					Id = rec.Id,
					StorageName = rec.StorageName,
					StorageIngridients = context.StorageIngridients
							.Where(recPC => recPC.StorageId == rec.Id)
							.Select(recPC => new StorageIngridientsView
							{
								Id = recPC.Id,
								StorageId = recPC.StorageId,
								IngridientId = recPC.IngridientId,
								IngridientName = recPC.Ingridient.IngridientName,
								Count = recPC.Count
							})
							.ToList()
				})
				.ToList();
			return result;
		}

		public StorageView GetElement(int id)
		{
			Storage element = context.Storages.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				return new StorageView
				{
					Id = element.Id,
					StorageName = element.StorageName,
					StorageIngridients = context.StorageIngridients
							.Where(recPC => recPC.StorageId == element.Id)
							.Select(recPC => new StorageIngridientsView
							{
								Id = recPC.Id,
								StorageId = recPC.StorageId,
								IngridientId = recPC.IngridientId,
								IngridientName = recPC.Ingridient.IngridientName,
								Count = recPC.Count
							})
							.ToList()
				};
			}
			throw new Exception("Элемент не найден");
		}

		public void AddElement(BindingStorage model)
		{
			Storage element = context.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName);
			if (element != null)
			{
				throw new Exception("Уже есть склад с таким названием");
			}
			context.Storages.Add(new Storage
			{
				StorageName = model.StorageName
			});
			context.SaveChanges();
		}

		public void UpdElement(BindingStorage model)
		{
			Storage element = context.Storages.FirstOrDefault(rec =>
										rec.StorageName == model.StorageName && rec.Id != model.Id);
			if (element != null)
			{
				throw new Exception("Уже есть склад с таким названием");
			}
			element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.StorageName = model.StorageName;
			context.SaveChanges();
		}

		public void DelElement(int id)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					Storage element = context.Storages.FirstOrDefault(rec => rec.Id == id);
					if (element != null)
					{
						// при удалении удаляем все записи о компонентах на удаляемом складе
						context.StorageIngridients.RemoveRange(
											context.StorageIngridients.Where(rec => rec.StorageId == id));
						context.Storages.Remove(element);
						context.SaveChanges();
					}
					else
					{
						throw new Exception("Элемент не найден");
					}
					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}
			}
		}
	}
}