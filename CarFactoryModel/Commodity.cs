using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFactory
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class Commodity
    {
        public int Id { get; set; }

		[Required]
		public string CommodityName { get; set; }

		[Required]
		public decimal Price { get; set; }

		[ForeignKey("CommodityId")]
        public virtual List<Booking> Bookings { get; set; }

        [ForeignKey("CommodityId")]
        public virtual List<CommodityIngridient> CommodityIngridients { get; set; }
	}
}
