using CarFactoryService.BindingModels;
using CarFactoryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryService.Interfaces
{
	public interface IReportService
	{
		void SaveCommodityPrice(ReportBindingModel model);

		List<StoragesLoadViewModel> GetStoragesLoad();

		void SaveStoragesLoad(ReportBindingModel model);

		List<ConsumerBookingsModel> GetConsumerBookings(ReportBindingModel model);

		void SaveConsumerBookings(ReportBindingModel model);
	}
}
