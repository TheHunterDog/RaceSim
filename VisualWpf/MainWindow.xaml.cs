using Controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Model;
using Controller;
using System.Windows.Threading;
using WPF;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ImageHandler.Initialize();
            Images.init();
            Data.Initialize(new Competition());
            Data.NextRaceEvent += OnNextRaceEvent;
            Data.StartNextRace();
        }
        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            this.TrackImage.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() =>
            {
                this.TrackImage.Source = null;
                this.TrackImage.Source = WPF.VisualizeWPF.DrawTrack(e.Track);
            }));
        }
        public void OnNextRaceEvent(object sender, NextRaceEventArgs e)
        {
            Images.Clear();
            //WPF.Visualize.Init(e.race);
            WPF.VisualizeWPF.Initialize(e.Race);
            DataContext d = new DataContext();

            e.Race.DriversChanged += OnDriversChanged;
            e.Race.DriversChanged += d.OnDriverChanged;

        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Shutdown();
        }
        private Batman batmanWindow = null;
        private Windesheim windesheimWindow = null;
        private void Open_WINdow(object sender, RoutedEventArgs e)
        {
            if(windesheimWindow == null)
            {
                windesheimWindow = new WPF.Windesheim();

            }
            windesheimWindow.Show();
        }

        private void open_OtherWindow(object sender, RoutedEventArgs e)
        {
            if (batmanWindow == null)
            {
                batmanWindow = new WPF.Batman();
            }
            batmanWindow.Show();
        }
    }
}
