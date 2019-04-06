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
    public class Metadata
    {
        [DataMember]
        public List<string> languages { get; set; }
        [DataMember]
        public string asp_retio { get; set; }
        [DataMember]
        public List<string> filming_locations { get; set; }
        [DataMember]
        public List<string> also_known_as { get; set; }
        [DataMember]
        public List<string> countries { get; set; }
        [DataMember]
        public string gross { get; set; }
        [DataMember]
        public List<string> sound_mix { get; set; }
        [DataMember]
        public string budget { get; set; }
    }
}
