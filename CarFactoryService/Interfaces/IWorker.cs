using CarFactoryService.Attributies;
using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    [CustomInterface("Интерфейс для работы с работниками")]
    public interface IWorker
    {
        [CustomMethod("Метод получения списка работников")]
        List<WorkerView> GetList();

        [CustomMethod("Метод получения работника по id")]
        WorkerView GetElement(int id);

        [CustomMethod("Метод добавления работника")]
        void AddElement(BindingWorkers model);

        [CustomMethod("Метод изменения данных по работнику")]
        void UpdElement(BindingWorkers model);

        [CustomMethod("Метод удаления работника")]
        void DelElement(int id);
    }
}
