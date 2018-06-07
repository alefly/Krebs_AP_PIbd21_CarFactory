using System.Runtime.Serialization;
namespace CarFactoryService.BindingModels
{
    [DataContract]
    public class BindingStorageIngridients
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StorageId { get; set; }
        [DataMember]
        public int IngridientId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
