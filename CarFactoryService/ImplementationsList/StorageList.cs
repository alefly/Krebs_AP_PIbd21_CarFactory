using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryService.ImplementationsList
{
    public class StorageList : IStorage
    {
        private ListDataSingleton source;

        public StorageList()
        {
            source = ListDataSingleton.GetInstance();
        }

        public List<StorageView> GetList()
        {
			List<StorageView> result = source.Storages
                .Select(rec => new StorageView
{
					Id = rec.Id,
					StorageName = rec.StorageName,
					StorageIngridients= source.StorageIngridients
                            .Where(recPC => recPC.StorageId == rec.Id)
                            .Select(recPC => new StorageIngridientsView
{
	Id = recPC.Id,
StorageId = recPC.StorageId,
	IngridientId = recPC.IngridientId,
	IngridientName = source.Ingridients
                                    .FirstOrDefault(recC => recC.Id == recPC.IngridientId)?.IngredientName,
	Count = recPC.Count
                            })
                            .ToList()
               })
                .ToList();
			return result;
        }

        public StorageView GetElement(int id)
        {
			Storage element = source.Storages.FirstOrDefault(rec => rec.Id == id);
			            if (element != null)
			{
				return new StorageView
				{
					Id = element.Id,
					StorageName = element.StorageName,
					StorageIngridients = source.StorageIngridients
                            .Where(recPC => recPC.StorageId == element.Id)
                            .Select(recPC => new StorageIngridientsView
{
	Id = recPC.Id,
	StorageId = recPC.StorageId,
	IngridientId = recPC.IngridientId,
	IngridientName = source.Ingridients
                                   .FirstOrDefault(recC => recC.Id == recPC.IngridientId)?.IngredientName,
	Count = recPC.Count
                            })
                            .ToList()
                };
			}
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BindingStorage model)
        {
            Storage element = source.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName);
			            if (element != null)
			{
				throw new Exception("Уже есть склад с таким названием");
			}
			int maxId = source.Storages.Count > 0 ? source.Storages.Max(rec => rec.Id) : 0;
			source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void UpdElement(BindingStorage model)
        {
			Storage element = source.Storages.FirstOrDefault(rec =>
rec.StorageName == model.StorageName && rec.Id != model.Id);
			           if (element != null)
			{
				throw new Exception("Уже есть склад с таким названием");
			}
			element = source.Storages.FirstOrDefault(rec => rec.Id == model.Id);
			            if (element == null)
			{
                throw new Exception("Элемент не найден");
            }
			element.StorageName = model.StorageName;
		}

        public void DelElement(int id)
        {
			Storage element = source.Storages.FirstOrDefault(rec => rec.Id == id);
			            if (element != null)
			{
				// при удалении удаляем все записи о компонентах на удаляемом складе
				source.StorageIngridients.RemoveAll(rec => rec.StorageId == id);
				source.Storages.Remove(element);
			}
            else
            {
				throw new Exception("Элемент не найден");
			}
            
        }
    }
}
