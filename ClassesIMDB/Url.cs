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
    public class Url
    {
        [DataMember]
        public string url { get; set; }
    }
}
