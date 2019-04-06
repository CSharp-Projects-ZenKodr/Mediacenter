using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesMediaCenter;

namespace MediaCenter
{
    /// <summary>
    /// Classe permettant de controler les vues
    /// du médiacenter
    /// </summary>
    public class MediaControleur : Sujet
    {
        private Mediatheque mediatheque;
        private Media media;

        /// <summary>
        /// Constructeur, initialise la médiatheque du controleur
        /// </summary>
        /// <param name="mediat">Mediatheque du mediacenter</param>
        public MediaControleur(Mediatheque mediat)
        {
            this.mediatheque = mediat;
        }

        public Media Media {
            get => media;

            set {
                media = value;
                this.Notifie(); // Notification aux fenetres, permettant à celle-ci de se mettre à jour
            }
        }
    }
}
