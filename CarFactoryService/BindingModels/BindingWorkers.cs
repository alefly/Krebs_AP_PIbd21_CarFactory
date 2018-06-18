using System.Runtime.Serialization;
namespace CarFactoryService.BindingModels
{
    [DataContract]
    public class BindingWorkers
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string WorkerName { get; set; }
    }
}
