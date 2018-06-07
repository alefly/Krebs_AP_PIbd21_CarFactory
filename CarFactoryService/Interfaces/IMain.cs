using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    public interface IMain
    {
        List<BookingView> GetList();

        void CreateOrder(BindingBooking model);

        void TakeOrderInWork(BindingBooking model);

        void FinishOrder(int id);

        void PayOrder(int id);

        void PutComponentOnStock(BindingStorageComponents model);
    }
}
