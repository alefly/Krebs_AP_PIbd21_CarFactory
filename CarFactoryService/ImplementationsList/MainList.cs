using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryService.WorkerList
{
	public class MainList : IMain
	{
		private ListDataSingleton source;

		public MainList()
		{
			source = ListDataSingleton.GetInstance();
		}

		public List<BookingView> GetList()
		{
			List<BookingView> result = source.Bookings
				.Select(rec => new BookingView
				{
					Id = rec.Id,
					ClientId = rec.ConsumerId,
					CommodityId = rec.CommodityId,
					WorkerId = rec.WorkerId,
					DateCreate = rec.DateCreate.ToLongDateString(),
					DateImplement = rec.DateWork?.ToLongDateString(),
					Status = rec.Status.ToString(),
					Count = rec.Count,
					Sum = rec.SumPrice,
					ClientName = source.Consumer
									.FirstOrDefault(recC => recC.Id == rec.ConsumerId)?.ConsumerName,
					CommodityName = source.Commodity
									.FirstOrDefault(recP => recP.Id == rec.CommodityId)?.CommodityName,
					WorkerName = source.Workers
									.FirstOrDefault(recI => recI.Id == rec.WorkerId)?.WorkerName
				})
			   .ToList();
			return result;
		}

		public void CreateOrder(BindingBooking model)
		{
			int maxId = source.Bookings.Count > 0 ? source.Bookings.Max(rec => rec.Id) : 0;
			source.Bookings.Add(new Booking
			{
				Id = maxId + 1,
				ConsumerId = model.ConsumerId,
				CommodityId = model.CommodityId,
				DateCreate = DateTime.Now,
				Count = model.Count,
				SumPrice = model.Sum,
				Status = BookingStatus.Принят
			});
		}

		public void TakeOrderInWork(BindingBooking model)
		{
			Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			// смотрим по количеству компонентов на складах
			var commodityIngridients = source.CommodityIngridients.Where(rec => rec.CommodityId == element.CommodityId);
			foreach (var commodityIngridient in commodityIngridients)
			{
				int countOnStorage = source.StorageIngridients
											.Where(rec => rec.IngridientId == commodityIngridient.IngridientId)
											.Sum(rec => rec.Count);
				if (countOnStorage < commodityIngridient.Count * element.Count)
				{
					var componentName = source.Ingridients
									.FirstOrDefault(rec => rec.Id == commodityIngridient.IngridientId);
					throw new Exception("Не достаточно компонента " + componentName?.IngredientName +
" требуется " + commodityIngridient.Count + ", в наличии " + countOnStorage);
				}
			}
			// списываем
			foreach (var commodityIngridient in commodityIngridients)
			{
				int countOnStoragies = commodityIngridient.Count * element.Count;
				var storageIngridients = source.StorageIngridients
												.Where(rec => rec.IngridientId == commodityIngridient.IngridientId);
				foreach (var storageIngridient in storageIngridients)
				{
					// компонентов на одном слкаде может не хватать
					if (storageIngridient.Count >= countOnStoragies)
					{
						storageIngridient.Count -= countOnStoragies;
						break;
					}
					else
					{
						countOnStoragies -= storageIngridient.Count;
						storageIngridient.Count = 0;
					}
				}
			}
			element.WorkerId = model.WorkerId;
			element.DateWork = DateTime.Now;
			element.Status = BookingStatus.Выполняется;
		}

		public void FinishOrder(int id)
		{
			Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.Status = BookingStatus.Готов;
		}

		public void PayOrder(int id)
		{
			Booking element = source.Bookings.FirstOrDefault(rec => rec.Id == id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.Status = BookingStatus.Оплачен;

		}

		public void PutComponentOnStock(BindingStorageComponents model)
		{
			StorageIngridient element = source.StorageIngridients
												.FirstOrDefault(rec => rec.StorageId == model.StorageId &&
rec.IngridientId == model.IngridientId);
			if (element != null)
			{
				element.Count += model.Count;
			}
			else
			{
				int maxId = source.StorageIngridients.Count > 0 ? source.StorageIngridients.Max(rec => rec.Id) : 0;
				source.StorageIngridients.Add(new StorageIngridient
				{
					Id = ++maxId,
					StorageId = model.StorageId,
					IngridientId = model.IngridientId,
					Count = model.Count
				});
			}
		}
	}
}
