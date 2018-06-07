using System.Runtime.Serialization; 

namespace CarFactoryService.ViewModels
{
    [DataContract]
    public class BookingView
    {
        [DataMember]
		public int Id { get; set; }
        [DataMember]
        public int ConsumerId { get; set; }
        [DataMember]
        public string ConsumerName { get; set; }
        [DataMember]
        public int CommodityId { get; set; }
        [DataMember]
        public string CommodityName { get; set; }
        [DataMember]
        public int? WorkerId { get; set; }
        [DataMember]
        public string WorkerName { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string DateImplement { get; set; }
    }
}
