using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ModtagBroadcastWcf
{
    [DataContract]
    public class Temperatur
    {
        [DataMember]
        public string Temp;

        [DataMember]
        public int Id;

        


    }
}