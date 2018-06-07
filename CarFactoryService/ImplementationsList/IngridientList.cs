using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryService.WorkerList
{
	public class IngridientList : IIngridient
	{
		private ListDataSingleton source;

		public IngridientList()
		{
			source = ListDataSingleton.GetInstance();
		}

		public List<IngridientView> GetList()
		{
			List<IngridientView> result = source.Ingridients
				.Select(rec => new IngridientView
				{
					Id = rec.Id,
					IngridientName = rec.IngridientName
				}).ToList();

			return result;
		}

		public IngridientView GetElement(int id)
		{
			Ingridient element = source.Ingridients.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				return new IngridientView
				{
					Id = element.Id,
					IngridientName = element.IngridientName
				};
			}
			throw new Exception("Элемент не найден");
		}

		public void AddElement(BindingIngridients model)
		{
			Ingridient element = source.Ingridients.FirstOrDefault(rec => rec.IngridientName == model.IngridientName);
			if (element != null)
			{
				throw new Exception("Уже есть компонент с таким названием");
			}
			int maxId = source.Ingridients.Count > 0 ? source.Ingridients.Max(rec => rec.Id) : 0;
			source.Ingridients.Add(new Ingridient
			{
				Id = maxId + 1,
				IngridientName = model.IngridientName
			});
		}

		public void UpdElement(BindingIngridients model)
		{
			Ingridient element = source.Ingridients.FirstOrDefault(rec =>
 rec.IngridientName == model.IngridientName && rec.Id != model.Id);
			if (element != null)
			{
				throw new Exception("Уже есть компонент с таким названием");
			}
			element = source.Ingridients.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.IngridientName = model.IngridientName;
		}

		public void DelElement(int id)
		{
			Ingridient element = source.Ingridients.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				source.Ingridients.Remove(element);
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}
	}
}
