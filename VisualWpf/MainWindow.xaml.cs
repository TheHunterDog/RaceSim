using System;
using System.Windows;
using System.Windows.Threading;
using Controller;
using Model;

namespace WPF;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private BasicStats? _basicStats;
    private Saboteur? _saboteur;
    private RaceStatsWindow? _raceStatsWindow;

    public MainWindow()
    {
        InitializeComponent();
        //ImageHandler.Initialize();
        Images.Init();
        Data.Initialize(new Competition());
        Data.NextRaceEvent += OnNextRaceEvent!;
        Data.StartNextRace(Data.Competition);
    }

    private void OnDriversChanged(object sender, DriversChangedEventArgs e)
    {
        TrackImage.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() =>
            {
                TrackImage.Source = null;
                TrackImage.Source = VisualizeWpf.DrawTrack(e.Track);
            }));
    }

    private void OnNextRaceEvent(object sender, NextRaceEventArgs e)
    {
        Images.Clear();
        //WPF.Visualize.Init(e.race);
        if (e.Race != null)
        {
            VisualizeWpf.Initialize(e.Race);
            var d = new DataContext();

            e.Race.DriversChanged += OnDriversChanged!;
            e.Race.DriversChanged += d.OnDriverChanged!;
        }
    }

    private void ExitClick(object sender, RoutedEventArgs e)
    {
        Close();
        Application.Current.Shutdown();
    }

    private void Open_Saboteur(object sender, RoutedEventArgs e)
    {
        _saboteur ??= new Saboteur();
        _saboteur?.Show();
    }

    private void Open_BasicStatsWindow(object sender, RoutedEventArgs e)
    {
        _basicStats ??= new BasicStats();
        _basicStats?.Show();
    }

    private void Open_RaceStatsWindow(object sender, RoutedEventArgs e)
    {
        _raceStatsWindow = new RaceStatsWindow();
        _raceStatsWindow?.Show();
    }
}