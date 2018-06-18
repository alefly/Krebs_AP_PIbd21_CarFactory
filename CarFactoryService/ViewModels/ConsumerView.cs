using System.Runtime.Serialization;

namespace CarFactoryService.ViewModels
{
    [DataContract]
    public class ConsumerView
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ConsumerName { get; set; }
    }
}
