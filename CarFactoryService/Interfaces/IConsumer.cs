using CarFactoryService.Attributies;
using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    [CustomInterface("Интерфейс для работы с клиентами")]
    public interface IConsumer
    {
        [CustomMethod("Метод получения списка клиентов")]
        List<ConsumerView> GetList();

        [CustomMethod("Метод получения клиента по id")]
        ConsumerView GetElement(int id);

        [CustomMethod("Метод добавления клиента")]
        void AddElement(BindingConsumer model);

        [CustomMethod("Метод изменения данных по клиенту")]
        void UpdElement(BindingConsumer model);

        [CustomMethod("Метод удаления клиента")]
        void DelElement(int id);
    }
}
