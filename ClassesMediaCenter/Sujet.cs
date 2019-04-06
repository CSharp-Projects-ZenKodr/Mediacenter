using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCenter
{
    /// <summary>
    /// Classe permettant l'abonnement, la résiliation, et la notification 
    /// </summary>
    public class Sujet
    {
        private List<IObserver> observers = new List<IObserver>();

        /// <summary>
        /// Ajout d'un Observer à la liste du controleur
        /// </summary>
        /// <param name="obs">Observer à abonner</param>
        public void Abonne(IObserver obs)
        {
            observers.Add(obs);
        }

        /// <summary>
        /// Fonction permettant de résilier l'abonnement de l'observer
        /// </summary>
        /// <param name="obs">Observer à résilier</param>
        public void Resilie(IObserver obs)
        {
            observers.Remove(obs);
        }

        /// <summary>
        /// Fonction permettant d'envoyer un ordre de mise à jour
        /// à tous ses abonnés
        /// </summary>
        protected void Notifie()
        {
            foreach(IObserver obs in observers)
            {
                obs.update(this);
            }
        }
    }
}
