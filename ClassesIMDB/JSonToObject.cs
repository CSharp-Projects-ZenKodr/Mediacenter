using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClassesMediaCenter
{
    /// <summary>
    /// Classe permettant d'obtenir les informations d'un media
    /// grâce à l'utilisation de l'api IMDb.
    /// </summary>
    public class JSonToObject
    {
        /// <summary>
        /// Méthode recherchant les informations d'un media dont le titre 
        /// est passé en paramètre. 
        /// </summary>
        /// <param name="titre">Titre du média dont on veut connaitre les informations</param>
        /// <returns>Un tableau de RootObject</returns>
        public static RootObject[] JSonToRootObject(string json)
        {            
            //Lecture du fichier.
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            
            //Déserialisation
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject[]));

            RootObject[] ro = ser.ReadObject(ms) as RootObject[];
            
            //Fermeture du stream.
            ms.Close();
            
            return ro;
        }

        /// <summary>
        /// Methode permettant de remplir les informations d'un médias à l'aide 
        /// d'un Root object contenant les informations
        /// </summary>
        /// <param name="ro">RootObject contenant les informations</param>
        /// <param name="media">media dont les informations sont à rentrées.</param>
        public static void RootObjectToMedia(RootObject ro, Media media)
        {            
            //Remplissage des valeurs du media pour le résultat donné par le fichier JSON.
            if (ro.length != "")
                media.Duree = int.Parse(ro.length);
            if (ro.title != "")
                media.Titre = ro.title;
            if (ro.director != "")
                media.Auteur = ro.director;
            if (ro.release_date != "")
                media.Annee = int.Parse(ro.release_date.Substring(0, 4));
        }
    }
}
