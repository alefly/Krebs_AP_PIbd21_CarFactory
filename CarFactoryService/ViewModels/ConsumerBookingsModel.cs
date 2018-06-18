using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CarFactoryService.ViewModels
{
    [DataContract]
	public class ConsumerBookingsModel
	{
        [DataMember]
        public string ConsumerName { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string CommodityName { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
        [DataMember]
        public string Status { get; set; }
	}
}
