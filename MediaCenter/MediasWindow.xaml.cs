using ClassesMediaCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace MediaCenter
{
    /// <summary>
    /// Logique d'interaction pour MediasWindow.xaml
    /// </summary>
    public partial class MediasWindow : Window, IObserver
    {
        private MediaControleur mControleur;
        private Mediatheque mMediatheque;

        /// <summary>
        /// Création de la fenetre et affichage de tous les médias de la médiatheque
        /// </summary>
        /// <param name="control">Instance du controleur.</param>
        /// <param name="mediat">Mediatheque initialisée avec le dossier de l'utilisateur.</param>
        public MediasWindow(MediaControleur control, Mediatheque mediat)
        {
            InitializeComponent();

            this.mControleur = control;
            this.mControleur.Abonne(this);
            this.mMediatheque = mediat;

            this.updateList();
        }

        /// <summary>
        /// Lorsque le bouton de lecture est appuyé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(this.mListMedias.SelectedItem != null)
            {
                // On remplace le media du Controleur par celui sélectionné
                this.mControleur.Media = (Media)this.mListMedias.SelectedItem;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Vous devez sélectionner un média", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Fonction n'étant pas utilisée
        /// </summary>
        /// <param name="expediteur"></param>
        public void update(Sujet expediteur)
        {
            
        }

        private void b_informations_Click(object sender, RoutedEventArgs e)
        {
            if(this.mListMedias.SelectedItem != null)
            {
                IMDBWindow imdb = new IMDBWindow((Media)this.mListMedias.SelectedItem, this.mMediatheque);
                imdb.ShowDialog();
                this.updateList();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Vous devez sélectionner un média", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateList()
        {
            this.mListMedias.Items.Clear();
            Media[] mMedias = mMediatheque.getMedias();

            foreach (Media media in mMedias)
                this.mListMedias.Items.Add(media); // Ajout des médias de la médiatheque dans la liste
           
        }
    }
}
