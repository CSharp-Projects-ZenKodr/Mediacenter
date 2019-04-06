using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCenter
{
    /// <summary>
    /// Interface permettant au controleur l'abonnement 
    /// et la mise à jour de la classe implémentant l'interface
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Méthode à redéfinir appelée par le Controlleur permettant
        /// de mettre à jour l'observateur.
        /// </summary>
        /// <param name="expediteur">Controleur envoyant l'information</param>
        void update(Sujet expediteur);
    }
}
