using System.Runtime.Serialization;

namespace CarFactoryService.ViewModels
{
    [DataContract]
    public class WorkerView
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string WorkerName { get; set; }
    }
}
