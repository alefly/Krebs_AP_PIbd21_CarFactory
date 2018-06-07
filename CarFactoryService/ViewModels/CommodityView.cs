using System.Collections.Generic;

namespace CarFactoryService.ViewModels
{
    public class CommodityView
    {
        public int Id { get; set; }

        public string CommodityName { get; set; }

        public decimal Price { get; set; }

        public List<CommodityIngridientView> CommodityIngridients { get; set; }
    }
}
