using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryService.ViewModels
{
	public class ConsumerBookingsModel
	{
		public string ConsumerName { get; set; }
		public string DateCreate { get; set; }
		public string CommodityName { get; set; }
		public int Count { get; set; }
		public decimal Sum { get; set; }
		public string Status { get; set; }
	}
}
