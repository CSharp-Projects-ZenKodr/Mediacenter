using Shell32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace ClassesMediaCenter
{
    public class Mediatheque
    {
        #region attributs et propriétés

        private string medias; //Variable contenant le dossier contenant les médias
        private string apercus; //Variable contenant le dossier contenant les apercus des medias
        private List<Media> listMedia = new List<Media>(); //Liste contenant tous les medias.

        private string[] audioExtensions = { // Tableau contenant les types possibles de fichiers audios
            ".wav", ".mid", ".midi", ".wma", ".mp3", ".ogg", ".rma", ".flac", ".aac"
        };

        private string[] videoExtensions = { // Tableau contenant les types possibles de fichiers vidéos
            ".avi", ".mov", ".mp4", ".mpg", ".divx", ".wmv"
        };


        public string Medias { get => medias; }
        public string Apercus { get => apercus; }

        #endregion
   
        /// <summary>
        /// Constructeur de la Mediatheque
        /// </summary>
        /// <param name="dossierApercus">dossier contenant les apercus</param> 
        /// <param name="dossierMedia">dossier contenant les Medias</param>
        public Mediatheque(String dossierMedia, String dossierApercus)
        {
            this.medias = dossierMedia;
            this.apercus = dossierApercus;
        }

        /// <summary>
        /// Constructeur ne prenant en compte qu'un dossier contenant des médias.
        /// </summary>
        /// <param name="dossierMedia"></param>
        public Mediatheque(String dossierMedia)
        {
            this.medias = dossierMedia;
        }


        /// <summary>
        /// Permet d'ajouter un media à la liste
        /// </summary>
        /// <param name="media">Media à ajouter à la liste</param>
        public void AddMedia(Media media){
            this.listMedia.Add(media);
        }

        /// <summary>
        /// Parcours du dossier des medias pour créer les différents médias 
        /// à partir des fichiers existants.
        /// </summary>
        public void AddMedia()
        {

            DirectoryInfo dir = new DirectoryInfo(this.medias); //Recherche du répertoire des médias
            FileInfo[] fichiers = dir.GetFiles(); //Obtension des informations sur chaques fichiers
            
            foreach (FileInfo fichier in fichiers) 
            {
                //Détection automatique du type de média
                bool audio = false;
                bool video = false;

                if (Array.IndexOf(this.audioExtensions, fichier.Extension.ToLower()) != -1)
                    audio = true;
                if (Array.IndexOf(this.videoExtensions, fichier.Extension.ToLower()) != -1)
                    video = true;

                int duree = 0;

                //Ajout du média à la liste
                this.listMedia.Add(new Media(fichier.FullName, audio, video, duree));
            }
        }

        /// <summary>
        /// Fonction permettant de supprimer un media de la mediatheque 
        /// </summary>
        /// <param name="media">Media à supprimer</param>
        public void Remove(Media media)
        {
            this.listMedia.Remove(media);
        }

        /// <summary>
        /// Méthode renvoyant un tableau contenant les médias de la médiathèque
        /// </summary>
        /// <returns>Tableau contenant les medias de la mediatheque</returns>
        public Media[] getMedias()
        {
            return this.listMedia.ToArray();
        }

        /// <summary>
        /// Méthode permettant de chercher les médias d'un auteur passé en paramètre
        /// </summary>
        /// <param name="auteur">Représente l'auteur des médias auxquels on veut avoir accès</param>
        /// <returns>Tableau contenant les medias de la mediatheque dont l'auteur est celui passé en paramètre</returns>
        public Media[] getMediasByAuthor(String auteur)
        {
            List<Media> mediaTab = new List<Media>(); //Ajout du média à la liste;

            //parcours des médias
            foreach (Media m in this.listMedia)
            {
                if (m.Auteur.ToLower() == auteur.ToLower()) //Recherche des médias ayant cet auteur
                    mediaTab.Add(m);
            }

            return mediaTab.ToArray();
        }

        /// <summary>
        /// Méthode permettant de chercher les médias pour un titre passé en paramètre
        /// </summary>
        /// <param name="titre">Représente le titre des médias auxquels on veut avoir accès</param>
        /// <returns>Tableau contenant les medias de la mediatheque dont le titre est celui passé en paramètre</returns>
        public Media[] getMediasByTitle(String titre)
        {
            List<Media> mediaTab = new List<Media>(); //Ajout du média à la liste;

            //parcours des médias
            foreach (Media m in this.listMedia)
            {
                if (m.Titre.ToLower() == titre.ToLower()) //Recherche des médias ayant cet auteur
                    mediaTab.Add(m);
            }

            return mediaTab.ToArray();
        }

        /// <summary>
        /// Méthode permettant de chercher les médias pour une année passé en paramètre
        /// </summary>
        /// <param name="annee">Représente l'année des médias auxquels on veut avoir accès</param>
        /// <returns>Tableau contenant les medias de la mediatheque dont l'année est celle passée en paramètre</returns>
        public Media[] getMediasByYear(int annee)
        {
            List<Media> mediaTab = new List<Media>(); //Ajout du média à la liste;

            //parcours des médias
            foreach (Media m in this.listMedia)
            {
                if (m.Annee == annee) //Recherche des médias ayant cet auteur
                    mediaTab.Add(m);
            }

            return mediaTab.ToArray();
        }
    }
}