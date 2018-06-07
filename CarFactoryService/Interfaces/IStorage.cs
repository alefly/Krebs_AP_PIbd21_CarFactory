using CarFactoryService.Attributies;
using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    [CustomInterface("Интерфейс для работы со складами")]
    public interface IStorage
    {
        [CustomMethod("Метод получения списка складов")]
        List<StorageView> GetList();

        [CustomMethod("Метод получения склада по id")]
        StorageView GetElement(int id);

        [CustomMethod("Метод добавления склада")]
        void AddElement(BindingStorage model);

        [CustomMethod("Метод изменения данных по складу")]
        void UpdElement(BindingStorage model);

        [CustomMethod("Метод удаления склада")]
        void DelElement(int id);
    }
}
