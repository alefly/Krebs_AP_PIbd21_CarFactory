using System.Runtime.Serialization;
namespace CarFactoryService.BindingModels
{
    [DataContract]
    public class BindingCommodityIngridient
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CommodityId { get; set; }
        [DataMember]
        public int IngridientId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
