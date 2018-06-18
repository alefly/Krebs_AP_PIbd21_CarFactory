using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryService.ImplementationsList
{
	public class WorkerList : IWorker
	{
		private ListDataSingleton source;

		public WorkerList()
		{
			source = ListDataSingleton.GetInstance();
		}

		public List<WorkerView> GetList()
		{
			List<WorkerView> result = source.Workers
				  .Select(rec => new WorkerView
				  {
					  Id = rec.Id,
					  WorkerName = rec.WorkerName
				  }).ToList();
			return result;
		}

		public WorkerView GetElement(int id)
		{
			Worker element = source.Workers.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				return new WorkerView
				{
					Id = element.Id,
					WorkerName = element.WorkerName
				};
			}
			throw new Exception("Элемент не найден");
		}

		public void AddElement(BindingWorkers model)
		{
			Worker element = source.Workers.FirstOrDefault(rec => rec.WorkerName == model.WorkerName);
			if (element != null)
			{
				throw new Exception("Уже есть сотрудник с таким ФИО");
			}
			int maxId = source.Workers.Count > 0 ? source.Workers.Max(rec => rec.Id) : 0;
			source.Workers.Add(new Worker
			{
				Id = maxId + 1,
				WorkerName = model.WorkerName
			});
		}

		public void UpdElement(BindingWorkers model)
		{
			Worker element = source.Workers.FirstOrDefault(rec =>
 rec.WorkerName == model.WorkerName && rec.Id != model.Id);
			if (element != null)
			{
				throw new Exception("Уже есть сотрудник с таким ФИО");
			}
			element = source.Workers.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.WorkerName = model.WorkerName;
		}

		public void DelElement(int id)
		{
			Worker element = source.Workers.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				source.Workers.Remove(element);
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}
	}
}
