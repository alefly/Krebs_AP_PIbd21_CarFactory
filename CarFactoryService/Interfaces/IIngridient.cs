using CarFactoryService.Attributies;
using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    [CustomInterface("Интерфейс для работы с компонентами")]
    public interface IIngridient
    {
        [CustomMethod("Метод получения списка компонент")]
        List<IngridientView> GetList();

        [CustomMethod("Метод получения компонента по id")]
        IngridientView GetElement(int id);

        [CustomMethod("Метод добавления компонента")]
        void AddElement(BindingIngridients model);

        [CustomMethod("Метод изменения данных по компоненту")]
        void UpdElement(BindingIngridients model);

        [CustomMethod("Метод удаления компонента")]
        void DelElement(int id);
    }
}
