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
	public class CommodityServiceDB : ICommodity
	{
		private CarFactoryDbContext context;

		public CommodityServiceDB(CarFactoryDbContext context)
		{
			this.context = context;
		}

		public List<CommodityView> GetList()
		{
			List<CommodityView> result = context.Commodities
				.Select(rec => new CommodityView
				{
					Id = rec.Id,
					CommodityName = rec.CommodityName,
					Price = rec.Price,
					CommodityIngridients = context.CommodityIngridients
							.Where(recPC => recPC.CommodityId == rec.Id)
							.Select(recPC => new CommodityIngridientView
							{
								Id = recPC.Id,
								CommodityId = recPC.CommodityId,
								IngridientId = recPC.IngridientId,
								IngridientName = recPC.Ingridient.IngridientName,
								Count = recPC.Count
							})
							.ToList()
				})
				.ToList();
			return result;
		}

		public CommodityView GetElement(int id)
		{
			Commodity element = context.Commodities.FirstOrDefault(rec => rec.Id == id);
			if (element != null)
			{
				return new CommodityView
				{
					Id = element.Id,
					CommodityName = element.CommodityName,
					Price = element.Price,
					CommodityIngridients = context.CommodityIngridients
							.Where(recPC => recPC.CommodityId == element.Id)
							.Select(recPC => new CommodityIngridientView
							{
								Id = recPC.Id,
								CommodityId = recPC.CommodityId,
								IngridientId = recPC.IngridientId,
								IngridientName = recPC.Ingridient.IngridientName,
								Count = recPC.Count
							})
							.ToList()
				};
			}
			throw new Exception("Элемент не найден");
		}

		public void AddElement(BindingCommodity model)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					Commodity element = context.Commodities.FirstOrDefault(rec => rec.CommodityName == model.CommodityName);
					if (element != null)
					{
						throw new Exception("Уже есть изделие с таким названием");
					}
					element = new Commodity
					{
						CommodityName = model.CommodityName,
						Price = model.Price
					};
					context.Commodities.Add(element);
					context.SaveChanges();
					// убираем дубли по компонентам
					var groupIngridients = model.CommodityIngridients
												.GroupBy(rec => rec.IngridientId)
												.Select(rec => new
												{
													IngridientId = rec.Key,
													Count = rec.Sum(r => r.Count)
												});
					// добавляем компоненты
					foreach (var groupIngridient in groupIngridients)
					{
						context.CommodityIngridients.Add(new CommodityIngridient
						{
							CommodityId = element.Id,
							IngridientId = groupIngridient.IngridientId,
							Count = groupIngridient.Count
						});
						context.SaveChanges();
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

		public void UpdElement(BindingCommodity model)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					Commodity element = context.Commodities.FirstOrDefault(rec =>
										rec.CommodityName == model.CommodityName && rec.Id != model.Id);
					if (element != null)
					{
						throw new Exception("Уже есть изделие с таким названием");
					}
					element = context.Commodities.FirstOrDefault(rec => rec.Id == model.Id);
					if (element == null)
					{
						throw new Exception("Элемент не найден");
					}
					element.CommodityName = model.CommodityName;
					element.Price = model.Price;
					context.SaveChanges();

					// обновляем существуюущие компоненты
					var compIds = model.CommodityIngridients.Select(rec => rec.IngridientId).Distinct();
					var updateIngridients = context.CommodityIngridients
													.Where(rec => rec.CommodityId == model.Id &&
														compIds.Contains(rec.IngridientId));
					foreach (var updateIngridient in updateIngridients)
					{
						updateIngridient.Count = model.CommodityIngridients
														.FirstOrDefault(rec => rec.Id == updateIngridient.Id).Count;
					}
					context.SaveChanges();
					context.CommodityIngridients.RemoveRange(
										context.CommodityIngridients.Where(rec => rec.CommodityId == model.Id &&
																			!compIds.Contains(rec.IngridientId)));
					context.SaveChanges();
					// новые записи
					var groupIngridients = model.CommodityIngridients
												.Where(rec => rec.Id == 0)
												.GroupBy(rec => rec.IngridientId)
												.Select(rec => new
												{
													IngridientId = rec.Key,
													Count = rec.Sum(r => r.Count)
												});
					foreach (var groupIngridient in groupIngridients)
					{
						CommodityIngridient elementPC = context.CommodityIngridients
												.FirstOrDefault(rec => rec.CommodityId == model.Id &&
																rec.IngridientId == groupIngridient.IngridientId);
						if (elementPC != null)
						{
							elementPC.Count += groupIngridient.Count;
							context.SaveChanges();
						}
						else
						{
							context.CommodityIngridients.Add(new CommodityIngridient
							{
								CommodityId = model.Id,
								IngridientId = groupIngridient.IngridientId,
								Count = groupIngridient.Count
							});
							context.SaveChanges();
						}
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

		public void DelElement(int id)
		{
			using (var transaction = context.Database.BeginTransaction())
			{
				try
				{
					Commodity element = context.Commodities.FirstOrDefault(rec => rec.Id == id);
					if (element != null)
					{
						// удаяем записи по компонентам при удалении изделия
						context.CommodityIngridients.RemoveRange(
											context.CommodityIngridients.Where(rec => rec.CommodityId == id));
						context.Commodities.Remove(element);
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