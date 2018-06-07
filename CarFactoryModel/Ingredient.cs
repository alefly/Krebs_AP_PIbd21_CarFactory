using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFactory
{
    /// <summary>
    /// Компонент, требуемый для изготовления изделия
    /// </summary>
    public class Ingridient
    {
        public int Id { get; set; }

		[Required]
		public string IngridientName { get; set; }

		[ForeignKey("IngridientId")]
        public virtual List<CommodityIngridient> CommodityIngridients { get; set; }

        [ForeignKey("IngridientId")]
        public virtual List<StorageIngridient> StorageIngridients { get; set; }
	}
}
