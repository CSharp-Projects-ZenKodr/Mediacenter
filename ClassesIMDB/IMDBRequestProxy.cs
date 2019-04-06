using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesMediaCenter
{
    /// <summary>
    /// Classe permettant de sauvegarder les requetes effectuées sur la base de données IMDB
    /// </summary>
    public class IMDBRequestProxy : IIDMBRequest
    {
        IMDBRequest mdbR = new IMDBRequest();
        Dictionary<string, string> dico = new Dictionary<string, string>();

        /// <summary>
        /// Méthode permettant d'obtenir le fichier JSON contenant les informations d'un média
        /// </summary>
        /// <param name="title">titre du média dont on veut les informations</param>
        /// <returns>JSON contenant les informations</returns>
        public async Task<string> GetInformation(string title)
        {
            if (title == null || title == "")
                throw new Exception("Le titre ne peux pas être vide."); 
            if (!dico.Keys.Contains(title)) // Si les informations du médias n'ont pas encore été cherché
                dico[title] = await mdbR.GetInformation(title);

            return dico[title];
        }
    }
}
