using CarFactory;
using CarFactoryService;
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
	public class IngridientServiceDB : IIngridient
	{
		private CarFactoryDbContext context;

		public IngridientServiceDB(CarFactoryDbContext context)
		{
			this.context = context;
		}

		public List<IngridientView> GetList()
		{
			List<IngridientView> result = context.Ingridients
				.Select(rec => new IngridientView
				{
					Id = rec.Id,
					IngridientName = rec.IngridientName
				})
				.ToList();
			return result;
		}

		public IngridientView GetElement(int id)
		{
			Ingridient element = context.Ingridients.FirstOrDefault(rec => rec.Id == id);
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
			Ingridient element = context.Ingridients.FirstOrDefault(rec => rec.IngridientName == model.IngridientName);
			if (element != null)
			{
				throw new Exception("Уже есть компонент с таким названием");
			}
			context.Ingridients.Add(new Ingridient
			{
				IngridientName = model.IngridientName
			});
			context.SaveChanges();
		}

		public void UpdElement(BindingIngridients model)
		{
			Ingridient element = context.Ingridients.FirstOrDefault(rec =>
										rec.IngridientName == model.IngridientName && rec.Id != model.Id);
			if (element != null)
			{
				throw new Exception("Уже есть компонент с таким названием");
			}
			element = context.Ingridients.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.IngridientName = model.IngridientName;
			context.SaveChanges();
		}

		public void DelElement(int id)
		{
			Ingridient element = context.Ingridients.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				context.Ingridients.Remove(element);
				context.SaveChanges();
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}
	}
}