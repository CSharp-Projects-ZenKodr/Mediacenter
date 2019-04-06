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
    public class RootObject
    {
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string content_rating { get; set; }
        [DataMember]
        public string original_title { get; set; }
        [DataMember]
        public Metadata metadata { get; set; }
        [DataMember]
        public string release_date { get; set; }
        [DataMember]
        public string director { get; set; }
        [DataMember]
        public Url url { get; set; }
        [DataMember]
        public string year { get; set; }
        [DataMember]
        public List<Trailer> trailer { get; set; }
        [DataMember]
        public string length { get; set; }
        [DataMember]
        public List<Cast> cast { get; set; }
        [DataMember]
        public string imdb_id { get; set; }
        [DataMember]
        public string rating { get; set; }
        [DataMember]
        public List<string> genre { get; set; }
        [DataMember]
        public string rating_count { get; set; }
        [DataMember]
        public string storyline { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public List<string> writers { get; set; }
        [DataMember]
        public List<string> stars { get; set; }
        [DataMember]
        public Poster poster { get; set; }
    }
}
