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
	public class ConsumerServiceDB : IConsumer
	{
		private CarFactoryDbContext context;

		public ConsumerServiceDB(CarFactoryDbContext context)
		{
			this.context = context;
		}

		public List<ConsumerView> GetList()
		{
			List<ConsumerView> result = context.Consumers
				.Select(rec => new ConsumerView
				{
					Id = rec.Id,
                    Mail = rec.Mail,
					ConsumerName = rec.ConsumerName
				})
				.ToList();
			return result;
		}

		public ConsumerView GetElement(int id)
		{
			Consumer element = context.Consumers.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				return new ConsumerView
				{
					Id = element.Id,
                    Mail = element.Mail,
                    ConsumerName = element.ConsumerName,
                    Messages = context.MessageInfos
                            .Where(recM => recM.ConsumerId == element.Id)
                            .Select(recM => new MessageInfoView
                            {
                        MessageId = recM.MessageId,
                        DateDelivery = recM.DateDelivery,
                        Subject = recM.Subject,
                        Body = recM.Body
                            })
                            .ToList()

                };
			}
			throw new Exception("Элемент не найден");
		}

		public void AddElement(BindingConsumer model)
		{
			Consumer element = context.Consumers.FirstOrDefault(rec => rec.ConsumerName == model.ConsumerName);
			if (element != null)
			{
				throw new Exception("Уже есть клиент с таким ФИО");
			}
			context.Consumers.Add(new Consumer
			{
                Mail = model.Mail,
				ConsumerName = model.ConsumerName
			});
			context.SaveChanges();
		}

		public void UpdElement(BindingConsumer model)
		{
			Consumer element = context.Consumers.FirstOrDefault(rec =>
									rec.ConsumerName == model.ConsumerName && rec.Id != model.Id);
			if (element != null)
			{
				throw new Exception("Уже есть клиент с таким ФИО");
			}
			element = context.Consumers.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
            element.Mail = model.Mail;
			element.ConsumerName = model.ConsumerName;
            context.SaveChanges();
		}

		public void DelElement(int id)
		{
			Consumer element = context.Consumers.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				context.Consumers.Remove(element);
				context.SaveChanges();
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}
	}
}