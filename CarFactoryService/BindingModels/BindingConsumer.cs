using System.Runtime.Serialization;
namespace CarFactoryService.BindingModels
{
    [DataContract]
    public class BindingConsumer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ConsumerName { get; set; }
    }
}
