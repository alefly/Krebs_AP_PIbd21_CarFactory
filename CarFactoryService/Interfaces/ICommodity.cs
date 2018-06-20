using CarFactoryService.Attributies;
using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    [CustomInterface("Интерфейс для работы с изделиями")]
    public interface ICommodity
    {
        [CustomMethod("Метод получения списка изделий")]
        List<CommodityView> GetList();

        [CustomMethod("Метод получения изделия по id")]
        CommodityView GetElement(int id);

        [CustomMethod("Метод добавления изделия")]
        void AddElement(BindingCommodity model);

        [CustomMethod("Метод изменения данных по изделию")]
        void UpdElement(BindingCommodity model);

        [CustomMethod("Метод удаления изделия")]
        void DelElement(int id);
    }
}
