using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryService.WorkDB
{
	public class MainServiceDB : IMain
	{
		private CarFactoryWebDbContext context;

		public MainServiceDB(CarFactoryWebDbContext context)
		{
			this.context = context;
		}

        public MainServiceDB()
        {
            this.context = new CarFactoryWebDbContext(); ;
        }


        public List<BookingView> GetList()
		{
			List<BookingView> result = context.Bookings
				.Select(rec => new BookingView
				{
					Id = rec.Id,
					ConsumerId = rec.ConsumerId,
					CommodityId = rec.CommodityId,
					WorkerId = rec.WorkerId,
					DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
								SqlFunctions.DateName("mm", rec.DateCreate) + " " +
								SqlFunctions.DateName("yyyy", rec.DateCreate),
					DateImplement = rec.DateWork == null ? "" :
										SqlFunctions.DateName("dd", rec.DateWork.Value) + " " +
										SqlFunctions.DateName("mm", rec.DateWork.Value) + " " +
										SqlFunctions.DateName("yyyy", rec.DateWork.Value),
					Status = rec.Status.ToString(),
					Count = rec.Count,
					Sum = rec.SumPrice,
					ConsumerName = rec.Consumer.ConsumerName,
					CommodityName = rec.Commodity.CommodityName,
					WorkerName = rec.Worker.WorkerName
				})
				.ToList();
			return result;
		}

		public void CreateBooking(BindingBooking model)
		{
			context.Bookings.Add(new Booking
			{
				ConsumerId = model.ConsumerId,
				CommodityId = model.CommodityId,
				DateCreate = DateTime.Now,
				Count = model.Count,
				SumPrice = model.Sum,
				Status = BookingStatus.Принят
			});
			context.SaveChanges();
		}

        public List<IngridientView> GetListIngr()
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

        public void TakeBookingInWork(BindingBooking model)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{

					Booking element = context.Bookings.FirstOrDefault(rec => rec.Id == model.Id);
					if (element == null)
					{
						throw new Exception("Элемент не найден");
					}
					var commodityIngridients = context.CommodityIngridients
												.Include(rec => rec.Ingridient)
												.Where(rec => rec.CommodityId == element.CommodityId);
					// списываем
					foreach (var commodityIngridient in commodityIngridients)
					{
						int countOnStorage = commodityIngridient.Count * element.Count;
						var storageIngridients = context.StorageIngridients
													.Where(rec => rec.IngridientId == commodityIngridient.IngridientId);
						foreach (var storageIngridient in storageIngridients)
						{
							// компонентов на одном слкаде может не хватать
							if (storageIngridient.Count >= countOnStorage)
							{
								storageIngridient.Count -= countOnStorage;
								countOnStorage = 0;
								context.SaveChanges();
								break;
							}
							else
							{
								countOnStorage -= storageIngridient.Count;
								storageIngridient.Count = 0;
								context.SaveChanges();
							}
						}
						if (countOnStorage > 0)
						{
							throw new Exception("Не достаточно компонента " +
								commodityIngridient.Ingridient.IngridientName + " требуется " +
								commodityIngridient.Count + ", не хватает " + countOnStorage);
						}
					}
					element.WorkerId = model.WorkerId;
					element.DateWork = DateTime.Now;
					element.Status = BookingStatus.Выполняется;
					context.SaveChanges();
					transaction.Commit();
				}
				catch (Exception)
				{
					transaction.Rollback();
					throw;
				}
			}
		}

		public void FinishBooking(int id)
		{
			Booking element = context.Bookings.FirstOrDefault(rec => rec.Id == id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.Status = BookingStatus.Готов;
			context.SaveChanges();
		}

		public void PayBooking(int id)
		{
			Booking element = context.Bookings.FirstOrDefault(rec => rec.Id == id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			element.Status = BookingStatus.Оплачен;
			context.SaveChanges();
		}

		public void PutIngridientOnStorage(BindingStorageIngridients model)
		{
			StorageIngridient element = context.StorageIngridients
												.FirstOrDefault(rec => rec.StorageId == model.StorageId &&
																	rec.IngridientId == model.IngridientId);
			if (element != null)
			{
				element.Count += model.Count;
			}
			else
			{
				context.StorageIngridients.Add(new StorageIngridient
				{
					StorageId = model.StorageId,
					IngridientId = model.IngridientId,
					Count = model.Count
				});
			}
			context.SaveChanges();
		}
	}
}
