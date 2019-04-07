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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediaCenter
{
    /// <summary>
    /// Logique d'interaction pour IMDBWindow.xaml
    /// </summary>
    public partial class IMDBWindow : Window
    {
        private IMDBRequestProxy imdb = new IMDBRequestProxy();
        private Mediatheque mMediatheque;
        private Media mMedia;
        private RootObject[] ros;

        public IMDBWindow(Media me, Mediatheque mt)
        {
            InitializeComponent();
            this.bValider.IsEnabled = false;
            this.mMediatheque = mt;
            this.mMedia = me;

            init();
        }

        private async void init()
        {
            string json = await imdb.GetInformation(this.mMedia.Titre);

            if(json == "error")
            {
                System.Windows.Forms.MessageBox.Show("Erreur lors du chargement des informations du média", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }else
            {
                this.lStatus.Content = "Terminé !";
                ros = JSonToObject.JSonToRootObject(json);

                foreach (RootObject ro in ros)
                    this.comboMedia.Items.Add(ro.title);
                this.bValider.IsEnabled = true;
            }
        }

        private void bValider_Click(object sender, RoutedEventArgs e)
        {
            int index = this.comboMedia.SelectedIndex;
            JSonToObject.RootObjectToMedia(this.ros[index], this.mMedia);
            System.Windows.Forms.MessageBox.Show("Le fichier a été modifié !", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
