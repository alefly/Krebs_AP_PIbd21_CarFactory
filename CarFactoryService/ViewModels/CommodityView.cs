using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarFactoryService.ViewModels
{
    [DataContract]
    public class CommodityView
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string CommodityName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<CommodityIngridientView> CommodityIngridients { get; set; }
    }
}
