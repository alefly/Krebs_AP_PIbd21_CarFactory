using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarFactoryService.ViewModels
{
    [DataContract]
    public class StorageView
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string StorageName { get; set; }
        [DataMember]
        public List<StorageIngridientsView> StorageIngridients { get; set; }
    }
}
