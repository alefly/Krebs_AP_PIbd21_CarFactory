using System.Runtime.Serialization;

namespace CarFactoryService.ViewModels
{
    [DataContract]
    public class CommodityIngridientView
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CommodityId { get; set; }
        [DataMember]
        public int IngridientId { get; set; }
        [DataMember]
        public string IngridientName { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
