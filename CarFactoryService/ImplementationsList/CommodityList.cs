using CarFactory;
using CarFactoryService.BindingModels;
using CarFactoryService.Interfaces;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactoryService.WorkerList
{
    public class CommodityList : ICommodity
    {
        private ListDataSingleton source;

        public CommodityList()
        {
            source = ListDataSingleton.GetInstance();
        }

        public List<CommodityView> GetList()
        {
			List<CommodityView> result = source.Commodity
                .Select(rec => new CommodityView
                {
				Id = rec.Id,
CommodityName = rec.CommodityName,
Price = rec.Price,
CommodityIngridients = source.CommodityIngridients
                            .Where(recPC => recPC.CommodityId == rec.Id)
                            .Select(recPC => new CommodityIngridientView
							{
	Id = recPC.Id,
	CommodityId = recPC.CommodityId,
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

        public CommodityView GetElement(int id)
        {
			Commodity element = source.Commodity.FirstOrDefault(rec => rec.Id == id);
			            if (element != null)
			{
				return new CommodityView
               {
					Id = element.Id,
CommodityName = element.CommodityName,
Price = element.Price,
CommodityIngridients = source.CommodityIngridients
                           .Where(recPC => recPC.CommodityId == element.Id)
                           .Select(recPC => new CommodityIngridientView
{
	Id = recPC.Id,
	CommodityId = recPC.CommodityId,
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

        public void AddElement(BindingCommodity model)
        {
           Commodity element = source.Commodity.FirstOrDefault(rec => rec.CommodityName == model.CommodityName);
			            if (element != null)
			{
				throw new Exception("Уже есть изделие с таким названием");
			}
			int maxId = source.Commodity.Count > 0 ? source.Commodity.Max(rec => rec.Id) : 0;
			source.Commodity.Add(new Commodity
            {
                Id = maxId + 1,
                CommodityName = model.CommodityName,
                Price = model.Price
            });
			// компоненты для изделия
			int maxPCId = source.CommodityIngridients.Count > 0 ?
source.CommodityIngridients.Max(rec => rec.Id) : 0;
			// убираем дубли по компонентам
			var groupComponents = model.CommodityIngridients
                                        .GroupBy(rec => rec.IngridientId)
                                        .Select(rec => new
                                        {
				IngridientId = rec.Key,
Count = rec.Sum(r => r.Count)
                                        });
			// добавляем компоненты
			foreach (var groupComponent in groupComponents)
			{
                source.CommodityIngridients.Add(new CommodityIngridient
                {
                    Id = ++maxPCId,
					CommodityId = maxId + 1,
					IngridientId = groupComponent.IngridientId,
					Count = groupComponent.Count
				});
            }
        }

        public void UpdElement(BindingCommodity model)
        {
            Commodity element = source.Commodity.FirstOrDefault(rec =>
rec.CommodityName == model.CommodityName && rec.Id != model.Id);
			            if (element != null)
			{
				throw new Exception("Уже есть изделие с таким названием");
			}
			element = source.Commodity.FirstOrDefault(rec => rec.Id == model.Id);
		            if (element == null)
			{
                throw new Exception("Элемент не найден");
            }
			element.CommodityName = model.CommodityName;
			element.Price = model.Price;
			
			int maxPCId = source.CommodityIngridients.Count > 0 ? source.CommodityIngridients.Max(rec => rec.Id) : 0;
			// обновляем существуюущие компоненты
			var compIds = model.CommodityIngridients.Select(rec => rec.IngridientId).Distinct();
			var updateComponents = source.CommodityIngridients
                                            .Where(rec => rec.CommodityId == model.Id &&
compIds.Contains(rec.IngridientId));
			            foreach (var updateComponent in updateComponents)
			{
				updateComponent.Count = model.CommodityIngridients
                                                .FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
			}
			source.CommodityIngridients.RemoveAll(rec => rec. CommodityId == model.Id &&
!compIds.Contains(rec.IngridientId));
			// новые записи
			var groupComponents = model.CommodityIngridients
                                        .Where(rec => rec.Id == 0)
                                        .GroupBy(rec => rec.IngridientId)
                                        .Select(rec => new
                                        {
				IngridientId = rec.Key,
Count = rec.Sum(r => r.Count)
                                        });
			            foreach (var groupComponent in groupComponents)
			{
				CommodityIngridient elementPC = source.CommodityIngridients
                                        .FirstOrDefault(rec => rec.CommodityId == model.Id &&
rec.IngridientId == groupComponent.IngridientId);
				                if (elementPC != null)
				{
					elementPC.Count += groupComponent.Count;
					                }
				                else
                {
					source.CommodityIngridients.Add(new CommodityIngridient
					{
						Id = ++maxPCId,
						CommodityId = model.Id,
						IngridientId = groupComponent.IngridientId,
						Count = groupComponent.Count
                    });
				}
            }
        }

        public void DelElement(int id)
        {
			Commodity element = source.Commodity.FirstOrDefault(rec => rec.Id == id);
			           if (element != null)
			{
				// удаяем записи по компонентам при удалении изделия
				source.CommodityIngridients.RemoveAll(rec => rec.CommodityId == id);
				source.Commodity.Remove(element);
			}
            else
            {
				throw new Exception("Элемент не найден");
			}
           
        }
    }
}
