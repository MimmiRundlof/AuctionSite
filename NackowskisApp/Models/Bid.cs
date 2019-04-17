using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace NackowskisApp.Models
{
    [DataContract]
    public class Bid
    {
        [DataMember]
        public int BudID { get; set; }
        [DataMember]
        [Display(Name = "Sum")]
        public int Summa { get; set; }
        [DataMember]
        public int AuktionID { get; set; }
        [DataMember]
        public object Budgivare { get; set; }

    }
}
