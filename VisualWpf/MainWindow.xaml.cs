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
            Data.Initialize(new Competition());
            Data.NextRaceEvent += OnNextRaceEvent;
            Data.NextRace();
        }
        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            this.TrackImage.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() =>
            {
                this.TrackImage.Source = null;
                this.TrackImage.Source = WPF.VisualizeWPF.DrawTrack(e.track);
            }));
        }
        public void OnNextRaceEvent(object sender, NextRaceEventArgs e)
        {
            Images.Clear();
            //WPF.Visualize.Init(e.race);
            WPF.VisualizeWPF.Initialize(e.race);

            e.race.DriversChanged += OnDriversChanged;

        }
    }
}
