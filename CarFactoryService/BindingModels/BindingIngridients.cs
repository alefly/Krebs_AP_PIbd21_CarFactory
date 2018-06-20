using System.Runtime.Serialization;
namespace CarFactoryService.BindingModels
{
    [DataContract]
    public class BindingIngridients
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string IngridientName { get; set; }
    }
}
