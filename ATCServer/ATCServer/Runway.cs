using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ATCServer
{
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "ATCServer.ContractType".
    [DataContract]
    public class Runway
    {
        //basic information about the runway
        [DataMember]
        public string RunwayID { get; set; }

        [DataMember]
        public bool Status { get; set; }

        public Runway(string runwayId)
        {
            this.RunwayID = runwayId;
            this.Status = true;
        }
    }
}
