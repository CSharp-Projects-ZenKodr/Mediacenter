using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClassesMediaCenter
{
    /// <summary>
    /// Classe permettant de requeter la base de données IMDB
    /// </summary>
    public class IMDBRequest : IIDMBRequest
    {
        /// <summary>
        /// Méthode permettant d'obtenir le fichier JSON contenant les informations d'un média
        /// </summary>
        /// <param name="title">Titre du média dont on veut les informations</param>
        /// <returns>JSON contenant les informations du média</returns>
        public async Task<string> GetInformation(string title)
        {
            if (title == null || title == "")
                throw new Exception("Le titre ne peux pas être vide.");

            // Création de l'objet et paramètrage de celui-ci
            HttpClient http = new HttpClient(); 
            http.Timeout = TimeSpan.FromHours(1);

            // Requete de la base données
            var response = await http.GetAsync("http://www.theimdbapi.org/api/find/movie?title=" + title);

            // Si la requete a renvoyé un résultat
            if (response.IsSuccessStatusCode)
            {
            
                var res = await response.Content.ReadAsStringAsync(); // On renvoit le résultat de la requete
                return res as string;
            }
            else
            {
                // Dans le cas où la recherche n'a pas aboutie, on utilsie le fichier
                // transformers.json pour montrer la fonction du programme.
                return File.ReadAllText(@"transformers.json"); 
            }
        }   
    }
}
