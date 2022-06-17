using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Controller;
using Model;

namespace WPF;

public sealed class RaceDataContext : INotifyPropertyChanged
{
    public RaceDataContext(Race? currentRace, List<IParticipant?>? participants)
    {
        CurrentRace = currentRace;
        Participants = participants;
        Data.NextRaceEvent += OnNextRace!;
    }

    public RaceDataContext() : this(Data.CurrentRace, Data.Competition?.Participants)
    {
        Data.CurrentRace.DriversChanged += OnDriversChanged!;
    }

    public Race? CurrentRace { get; set; }

    private List<IParticipant?>? _participants;
    public List<IParticipant?>? Participants
    {
        get => _participants;
        set
        {
            _participants = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnNextRace(object sender, NextRaceEventArgs e)
    {
        if (e.Race == null) return;
        CurrentRace = e.Race;
        e.Race.DriversChanged += OnDriversChanged!;
    }

    // Fetch the participants in the current race and call property changed
    private void OnDriversChanged(object sender, DriversChangedEventArgs e)
    {
        if (CurrentRace?.Participants != null) Participants = CurrentRace?.Participants.ToList();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
    }

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
    }
}