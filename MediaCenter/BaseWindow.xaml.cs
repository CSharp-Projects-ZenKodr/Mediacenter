using ClassesMediaCenter;
using Microsoft.Win32;
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
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace MediaCenter
{
    /// <summary>
    /// Fenetre permettant rechercher le dossier de la mediatheque,
    /// de lancer la fenetre d'affichage des médias et le lecteur.
    /// </summary>
    public partial class BaseWindow : Window
    {
        private string cheminMedia = "";

        public BaseWindow()
        {
            InitializeComponent();
            if (File.Exists(@"mediatheque.txt"))
            {
                StreamReader sr = new StreamReader(@"mediatheque.txt");
                cheminMedia = sr.ReadLine();
                sr.Close();

                this.launchWindow();
            }
        }

        /// <summary>
        /// Fonction appelée lorsque l'utilisateur veut ouvrir un dossier
        /// Permet de lancer les deux fenetres d'affichages.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bOpen_Click(object sender, RoutedEventArgs e)
        {
            // Ouverture du dialog permettant de choisir un dossier
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            try
            {
                StreamWriter sw = new StreamWriter(@"mediatheque.txt", false, Encoding.Unicode);
                sw.WriteLine(folderDialog.SelectedPath);
                sw.Close();
            }
            catch(Exception exc)
            {
                System.Windows.Forms.MessageBox.Show("Erreur lords de l'écriture du fichier, vérifiez la justesse du chemin.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cheminMedia = folderDialog.SelectedPath;
            this.launchWindow();
        }

        private void launchWindow()
        {
            try
            {
                // Initialisation de la médiatheque
                Mediatheque mt = new Mediatheque(cheminMedia);
                mt.AddMedia();

                // Création du controleur
                MediaControleur mc = new MediaControleur(mt);

                // Création des fenetres et affichage
                MainWindow mainwindow = new MainWindow(mc);
                MediasWindow mediaWindow = new MediasWindow(mc, mt);
                mainwindow.Show();
                mediaWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Le chemin spécifié est introuvable, vérifiez le chemin indiqué est correct, ou si il possède des accents.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
