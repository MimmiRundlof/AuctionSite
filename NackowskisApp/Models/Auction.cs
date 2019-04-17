using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NackowskisApp.Models
{
    [DataContract]
    public class Auction
    {
        [DataMember]
        public int AuktionID { get; set; }
        [DataMember]
        public string Titel { get; set; }
        [DataMember]
        public string Beskrivning { get; set; }
        [DataMember]
        public string StartDatum { get; set; }
        [DataMember]
        public string SlutDatum { get; set; }
        [DataMember]
        public int Gruppkod { get; set; }
        [DataMember]
        public int Utropspris { get; set; }
        [DataMember]
        public object SkapadAv { get; set; }


    }
}
