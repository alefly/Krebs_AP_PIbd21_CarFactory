using System.Runtime.Serialization;

namespace CarFactoryService.BindingModels
{
    [DataContract]
    public class BindingBooking
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ConsumerId { get; set; }
        [DataMember]
        public int CommodityId { get; set; }
        [DataMember]
        public int? WorkerId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
