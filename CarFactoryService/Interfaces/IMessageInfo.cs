using CarFactoryService.Attributies;
using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryService.Interfaces
{
    [CustomInterface("Интерфейс для работы с письмами")]
    public interface IMessageInfo
    {
        [CustomMethod("Метод дя получения списка писем")]
        List<MessageInfoView> GetList();

        [CustomMethod("Метод получения письма по id")]
        MessageInfoView GetElement(int id);

        [CustomMethod("Метод добавления письма")]
        void AddElement(BindingMessageInfo model);
    }
}