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
	public class WorkerServiceDB : IWorker
	{
		private CarFactoryWebDbContext context;

		public WorkerServiceDB(CarFactoryWebDbContext context)
		{
			this.context = context;
		}

        public WorkerServiceDB()
        {
            this.context = new CarFactoryWebDbContext(); ;
        }

        public List<WorkerView> GetList()
		{
			List<WorkerView> result = context.Workers
				.Select(rec => new WorkerView
				{
					Id = rec.Id,
					WorkerName = rec.WorkerName
				})
				.ToList();
			return result;
		}

		public WorkerView GetElement(int id)
		{
			Worker element = context.Workers.FirstOrDefault(rec => rec.Id == id);
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
			Worker element = context.Workers.FirstOrDefault(rec => rec.WorkerName == model.WorkerName);
			if (element != null)
			{
				throw new Exception("Уже есть сотрудник с таким ФИО");
			}
			context.Workers.Add(new Worker
			{
				WorkerName = model.WorkerName
			});
			context.SaveChanges();
		}

		public void UpdElement(BindingWorkers model)
		{
			Worker element = context.Workers.FirstOrDefault(rec =>
										rec.WorkerName == model.WorkerName && rec.Id != model.Id);
			if (element != null)
			{
				throw new Exception("Уже есть сотрудник с таким ФИО");
			}
			element = context.Workers.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.WorkerName = model.WorkerName;
			context.SaveChanges();
		}

		public void DelElement(int id)
		{
			Worker element = context.Workers.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				context.Workers.Remove(element);
				context.SaveChanges();
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}
	}
}
