using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFactory
{
    /// <summary>
    /// Хранилиище компонентов в магазине
    /// </summary>
    public class Storage
    {
        public int Id { get; set; }

		[Required]
		public string StorageName { get; set; }

		[ForeignKey("StorageId")]
        public virtual List<StorageIngridient> StorageIngridients { get; set; }
	}
}
