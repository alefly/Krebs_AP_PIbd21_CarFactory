using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    public interface ICommodity
    {
        List<CommodityView> GetList();

        CommodityView GetElement(int id);

        void AddElement(BindingCommodity model);

        void UpdElement(BindingCommodity model);

        void DelElement(int id);
    }
}
