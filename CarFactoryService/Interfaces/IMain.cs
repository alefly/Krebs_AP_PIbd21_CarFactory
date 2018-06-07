using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System.Collections.Generic;

namespace CarFactoryService.Interfaces
{
    public interface IMain
    {
        List<BookingView> GetList();

        void CreateBooking(BindingBooking model);

        void TakeBookingInWork(BindingBooking model);

        void FinishBooking(int id);

        void PayBooking(int id);

        void PutIngridientOnStorage(BindingStorageIngridients model);
    }
}
