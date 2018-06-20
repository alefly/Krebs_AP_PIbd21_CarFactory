using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryService.ViewModels
{
    [DataContract]
    public class MessageInfoView
    {
        [DataMember]
        public string MessageId { get; set; }

        [DataMember]
        public string ConsumerName { get; set; }

        [DataMember]
        public DateTime DateDelivery { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }
    }
}
