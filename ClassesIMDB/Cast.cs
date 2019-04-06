using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClassesMediaCenter
{
    /// <summary>
    /// Classe servant uniquement à contenir les informations de déserialisation
    /// des fichiers JSON fournit par le service IMDn
    /// </summary>
    [DataContract]
    public class Cast
    {
        [DataMember]
        public string character { get; set; }
        [DataMember]
        public string image { get; set; }
        [DataMember]
        public string link { get; set; }
        [DataMember]
        public string name { get; set; }
    }
}
