using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CarFactoryService.ViewModels
{
    [DataContract]
	public class StoragesLoadViewModel
	{
        [DataMember]
        public string StorageName { get; set; }
        [DataMember]
        public int TotalCount { get; set; }
        [DataMember]
		public List<StorageIngridientLoadViewModel> Ingridients { get; set; }
	}

	[DataContract]
	public class StorageIngridientLoadViewModel
	{
		[DataMember]
		public string IngridientName { get; set; }

		[DataMember]
		public int Count { get; set; }
	}
}
