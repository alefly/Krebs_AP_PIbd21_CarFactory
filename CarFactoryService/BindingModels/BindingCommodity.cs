using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarFactoryService.BindingModels
{
    [DataContract]
     public class BindingCommodity
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string CommodityName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<BindingCommodityIngridient> CommodityIngridients { get; set; }
    }
}
