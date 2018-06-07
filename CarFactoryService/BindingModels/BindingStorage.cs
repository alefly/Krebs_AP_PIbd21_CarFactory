using System.Runtime.Serialization;

namespace CarFactoryService.BindingModels
{
    [DataContract]
    public class BindingStorage
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string StorageName { get; set; }
    }
}
