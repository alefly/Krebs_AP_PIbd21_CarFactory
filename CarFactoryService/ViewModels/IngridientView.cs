using System.Runtime.Serialization;

namespace CarFactoryService.ViewModels
{
    [DataContract]
    public class IngridientView
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string IngridientName { get; set; }
    }
}
