using Shell32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesMediaCenter
{
    public class Media
    {
        #region attributs et propriétés

        private String fichierMedia;
        private String fichierApercu;
        private String titre;
        private String auteur;
        private int annee;
        private int duree;
        private bool video;
        private bool audio;

        public String FichierMedia { get => fichierMedia; }
        public String FichierApercu { get => fichierApercu; }
        public String Titre { get => titre; set => titre = value; }
        public String Auteur { get => auteur; set => auteur = value; }
        public int Annee { get => annee; set => annee = value; }
        public bool Audio { get => audio; }
        public bool Video { get => video; }
        public int Duree { get => duree; set => duree = value; }
       
        #endregion
        
        /// <summary>
        /// Constructeur du media
        /// </summary>
        /// <param name="fichier">Chemin du fichier</param>
        /// <param name="audio"></param>    
        /// <param name="video"></param>
        /// <param name="duree">Durée du média</param>
        public Media(String fichier, bool audio, bool video, int duree)
        {
            //Traitement du chemin du fichier
            this.fichierMedia = fichier;

            //Récupération du nom du fichier
            System.IO.FileInfo fi= new System.IO.FileInfo(fichier);

            this.titre = fi.Name;

            this.titre = Convert.ToString(this.titre[0]).ToUpper() + this.titre.Substring(1);
            int point = this.titre.LastIndexOf('.');
            if(point > 0)
                this.titre = this.titre.Substring(0, this.titre.LastIndexOf('.'));
            
                
            //Récupération des informations sur le type de média et sa durée.
            this.audio = audio;
            this.video = video;
            this.duree = duree;
        }


        /// <summary>
        /// Fonction ToString d'un média renvoyant les informations sur son titre, auteur et année mises en forme.
        /// </summary>
        /// <returns>String contenant le résultat</returns>
        public override String ToString() 
        {
            return this.titre + " (" + this.auteur + ", " + this.annee + ")";
        }
    }
}
