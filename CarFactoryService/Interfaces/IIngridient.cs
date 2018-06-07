using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    public interface IIngridient
    {
        List<IngridientView> GetList();

        IngridientView GetElement(int id);

        void AddElement(BindingIngridients model);

        void UpdElement(BindingIngridients model);

        void DelElement(int id);
    }
}
