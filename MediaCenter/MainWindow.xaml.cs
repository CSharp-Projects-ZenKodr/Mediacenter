using ClassesMediaCenter;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MediaCenter
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private bool fullscreen = false; //Variable indiquant si la fenetre est en plein écran
        private TimeSpan posPause; //Position du média lors de la pause
        private DispatcherTimer temps; //Timer permettant d'avancer le temps du média
        private bool pause = false;
        private MediaControleur mControleur;

        /// <summary>
        /// Constructeur de la fenetre
        /// </summary>
        public MainWindow(MediaControleur mCont)
        {
            InitializeComponent();
            this.mControleur = mCont;
            this.mControleur.Abonne(this);
        }

        /// <summary>
        /// Action du bouton fullScreen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_FullScreen_Click(object sender, RoutedEventArgs e) 
        {
            this.setFullScreen();
        }

        /// <summary>
        /// Action pour mettre en plein écran.
        /// </summary>
        private void setFullScreen()
        {
            if (this.fullscreen && this.v_MediaElement.HasVideo)
            {
                this.Topmost = true;
                this.WindowStyle = WindowStyle.None;
                this.WindowState = WindowState.Maximized;
                this.fullscreen = !this.fullscreen;

                //Colorer l'arrière plan en noir.
                this.menu.Visibility = Visibility.Hidden; //Cacher le menu
                this.dockPanel.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                this.grid.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                this.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            else if (!this.fullscreen && this.v_MediaElement.HasVideo)
            {
                this.Topmost = false;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Normal;
                this.fullscreen = !this.fullscreen;

                ///Colorer l'arrière plan en blanc.
                this.menu.Visibility = Visibility.Visible; //Afficher le menu
            }
        }

        /// <summary>
        /// Action du bouton play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_Play_Click(object sender, RoutedEventArgs e) 
        {
            if (this.v_MediaElement.HasVideo || this.v_MediaElement.HasAudio) //Si le média est vidéo ou audio
            {
                this.pause = false;
                this.v_MediaElement.LoadedBehavior = MediaState.Play; //Lancement du média
                this.temps.Start(); //Configuration de l'intervale du média
                if (posPause != TimeSpan.Zero)  //Si le média était en pause
                {
                    this.v_MediaElement.Position = posPause; //La position du média à celle avant la pause.
                    posPause = TimeSpan.Zero; //On donne la valeur null à PosPause.
                }
            }
        }

        /// <summary>
        /// Action du bouton pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_Pause_Click(object sender, RoutedEventArgs e) 
        {
            if(this.v_MediaElement.HasVideo || this.v_MediaElement.HasAudio)
            {
                this.pause = true;
                this.posPause = this.v_MediaElement.Position; //Sauvegarde de la position du média.
                this.v_MediaElement.LoadedBehavior = MediaState.Stop; //Changement du comportement
                this.temps.Stop(); //On stoppe le timer.
            }
        }

        /// <summary>
        /// Action sur le slider de volume.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slide_Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) 
        {
            this.v_MediaElement.Volume = Slide_Volume.Value; //Le volume du média prend la valeur du slider.
        }

        /// <summary>
        /// Action du bouton ouvrir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Open_Click(object sender, RoutedEventArgs e) 
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Open a file to read"; // Default file name 
            dialog.Filter = "All Files (*.*)|*.*"; // Filter files by extension  

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                this.v_MediaElement.Source = new Uri(dialog.FileName);
            }
        }

        /// <summary>
        /// Action à chaque tick du timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e) 
        {
            updatePosition();
        }

        /// <summary>
        /// Action du bouton quitter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MI_Exit_Click(object sender, RoutedEventArgs e) 
        {
            this.Close();
        }

        /// <summary>
        /// Action lorsqu'un média est ouvert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void v_MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (this.v_MediaElement.NaturalDuration.HasTimeSpan) //Si le média est minuté
            {
                if (this.v_MediaElement.HasVideo || this.v_MediaElement.HasAudio) //Si le média est audio ou vidéo.
                {
                    //Activation des boutons
                    this.Slide_Avancement.IsEnabled = true;
                    this.b_Pause.IsEnabled = true;
                    this.b_Fast.IsEnabled = true;
                    this.b_Play.IsEnabled = true;
                    this.b_Stop.IsEnabled = true;
                    this.b_Slow.IsEnabled = true;
                    this.Slide_Volume.IsEnabled = true;

                    this.Slide_Volume.Maximum = 1;
                    this.Slide_Volume.Minimum = 0;
                    this.Slide_Volume.Value = 0.5;
                    this.v_MediaElement.Volume = this.Slide_Volume.Value;

                    //Réglage de la position du slider par rapport à la durée du média
                    this.Slide_Avancement.Minimum = 0;
                    this.Slide_Avancement.Maximum = this.v_MediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                    this.Slide_Avancement.Visibility = Visibility.Visible;
                    this.v_MediaElement.LoadedBehavior = MediaState.Play;

                    this.posPause = TimeSpan.FromMilliseconds(0);
                    //Lancement du timer.
                    temps = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromSeconds(0.1)
                    };
                    temps.Tick += new EventHandler(timer_Tick);
                    temps.IsEnabled = true;
                    this.temps.Start();
                }
                if(this.v_MediaElement.HasVideo)
                    this.b_FullScreen.IsEnabled = true;
            }
        }

        /// <summary>
        /// Met à jour les composants de la fenetre
        /// </summary>
        private void updatePosition() 
        { 
            this.Slide_Avancement.Value = this.v_MediaElement.Position.TotalSeconds; //Mise à jour du slide avancement.
            this.Tb_Pos.Text = this.v_MediaElement.Position.ToString(@"hh\:mm\:ss"); //
        }

        /// <summary>
        /// Action bouton Stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_Stop_Click(object sender, RoutedEventArgs e) 
        {
            //On désactive les boutons.
            this.v_MediaElement.Source = null; 
            this.temps.Stop();
            this.Slide_Avancement.IsEnabled = false;
            this.b_Pause.IsEnabled = false;
            this.b_Fast.IsEnabled = false;
            this.b_FullScreen.IsEnabled = false;
            this.b_Play.IsEnabled = false;
            this.b_Stop.IsEnabled = false;
            this.b_Slow.IsEnabled = false;
            this.Slide_Volume.IsEnabled = false;
            this.Tb_Pos.Text = "";
        }

        /// <summary>
        /// Action bouton slow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_Slow_Click(object sender, RoutedEventArgs e) 
        {
            if(this.v_MediaElement.HasVideo || this.v_MediaElement.HasAudio)
                this.v_MediaElement.SpeedRatio -= 0.5; //on ralentit de 0.5 le media
        }

        /// <summary>
        /// Action bouton fast
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_Fast_Click(object sender, RoutedEventArgs e) 
        {
            if (this.v_MediaElement.HasVideo || this.v_MediaElement.HasAudio)
                this.v_MediaElement.SpeedRatio += 0.5;//on accélère de 0.5 le media
        }

        /// <summary>
        /// Action bouton gauche de la souris sur le slider.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slide_Avancement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.v_MediaElement.HasVideo || this.v_MediaElement.HasAudio) 
            {
                //Mise en pause de la lecture et du timer
                this.v_MediaElement.LoadedBehavior = MediaState.Pause; 
                //this.temps.Interval = TimeSpan.FromHours(100);
                this.temps.Stop();
            }
                
        }

        /// <summary>
        /// Action bouton gauche de la souris sur le slider.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slide_Avancement_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.v_MediaElement.HasVideo || this.v_MediaElement.HasAudio)
            {
                this.v_MediaElement.Position = TimeSpan.FromSeconds(this.Slide_Avancement.Value); //On met à jour la position du média
                //this.temps.Interval = TimeSpan.FromSeconds(0.1); //On remet en marche le timer.
                this.temps.Start(); 
                this.v_MediaElement.LoadedBehavior = MediaState.Play; //On relance la lecture.
            }
        }

        /// <summary>
        /// évènement du clavier sur la fenetre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space: //Pour la touche espace 
                    {
                        //On met en pause ou on relance la lecture.
                        if (this.pause) {
                            this.b_Play_Click(sender, e);
                        }
                        else
                        {
                            this.b_Pause_Click(sender, e); 
                        }
                        break;
                    }
                case Key.Escape: //Pour la touche espace 
                    {
                        if (!this.fullscreen)
                        {
                            this.setFullScreen();
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Méthode permettant de changer de média lors d'un évènement
        /// </summary>
        /// <param name="expediteur">Sujet ayant envoyé l'évènement</param>
        public void update(Sujet expediteur)
        {
            if (temps != null)
                temps.Stop();
            this.v_MediaElement.Source = new Uri(this.mControleur.Media.FichierMedia);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.mControleur.Resilie(this);
        }

        private void MI_MediaFolder_Click(object sender, RoutedEventArgs e)
        {
            // Ouverture du dialog permettant de choisir un dossier
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            try
            {
                StreamWriter sw = new StreamWriter(@"mediatheque.txt", false, Encoding.ASCII);
                sw.WriteLine(folderDialog.SelectedPath);
                sw.Close();
            }
            catch (Exception exc)
            {

            }

            System.Windows.Forms.MessageBox.Show("Le dossier contenant les fichiers a été modifié, les réglages seront effectifs au redémarage !", "Modification du dossier des médias", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}