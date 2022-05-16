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
    private Batman? _batmanWindow;
    private Leaderboard? _leaderboard;
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

    private void Open_Leaderboard(object sender, RoutedEventArgs e)
    {
        _leaderboard ??= new Leaderboard();
        _leaderboard?.Show();
    }

    private void BatmanWindow(object sender, RoutedEventArgs e)
    {
        _batmanWindow ??= new Batman();
        _batmanWindow?.Show();
    }

    private void RaceStatsWindow(object sender, RoutedEventArgs e)
    {
        _raceStatsWindow ??= new RaceStatsWindow();
        _raceStatsWindow?.Show();
    }
}