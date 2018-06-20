using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CarFactoryService.BindingModels
{
    [DataContract]
    public class ReportBindingModel
	{
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public DateTime? DateFrom { get; set; }
        [DataMember]
        public DateTime? DateTo { get; set; }
	}
}
