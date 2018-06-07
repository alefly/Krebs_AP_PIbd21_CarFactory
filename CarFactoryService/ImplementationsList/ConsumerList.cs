using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryService.ImplementationsList
{
	public class ConsumerList : IConsumer
	{
		private ListDataSingleton source;

		public ConsumerList()
		{
			source = ListDataSingleton.GetInstance();
		}

		public List<ConsumerView> GetList()
		{
			List<ConsumerView> result = source.Consumer
				.Select(rec => new ConsumerView
				{
					Id = rec.Id,
					ConsumerName = rec.ConsumerName
				}).ToList();
				return result;
		}

		public ConsumerView GetElement(int id)
		{
			Consumer element = source.Consumer.FirstOrDefault(rec => rec.Id == id);
			if(element != null)
			{ 
				return new ConsumerView
				{ 
					Id = element.Id,
					ConsumerName = element.ConsumerName
					};
			}
			throw new Exception("Элемент не найден");
		}

		public void AddElement(BindingConsumer model)
		{
			Consumer element = source.Consumer.FirstOrDefault(rec => rec.ConsumerName == model.ConsumerName);
			if (element != null)
			{
				throw new Exception("Уже есть клиент с таким ФИО");
			}
			int maxId = source.Consumer.Count > 0 ? source.Consumer.Max(rec => rec.Id) : 0;
			source.Consumer.Add(new Consumer
			{
				Id = maxId + 1,
				ConsumerName = model.ConsumerName 
			});
		}

		public void UpdElement(BindingConsumer model)
		{
			Consumer element = source.Consumer.FirstOrDefault(rec =>
			rec.ConsumerName == model.ConsumerName && rec.Id != model.Id);
			if (element != null)
			{
				throw new Exception("Уже есть клиент с таким ФИО");
			}
			element = source.Consumer.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.ConsumerName = model.ConsumerName;
		}

		public void DelElement(int id)
		{
			Consumer element = source.Consumer.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				source.Consumer.Remove(element);
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}
	}
}
