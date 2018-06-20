using CarFactoryService.Attributies;
using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IMain
    {
        [CustomMethod("Метод получения списка заказов")]
        List<BookingView> GetList();

        [CustomMethod("Метод создания заказа")]
        void CreateBooking(BindingBooking model);

        [CustomMethod("Метод передачи заказа в работу")]
        void TakeBookingInWork(BindingBooking model);

        [CustomMethod("Метод передачи заказа на оплату")]
        void FinishBooking(int id);

        [CustomMethod("Метод фиксирования оплаты по заказу")]
        void PayBooking(int id);

        [CustomMethod("Метод пополнения компонент на складе")]
        void PutIngridientOnStorage(BindingStorageIngridients model);
    }
}
