using Model;

namespace Controller;

public static class Data
{
    #region props

    public static Competition? Competition { get; private set; }
    public static Race? CurrentRace { get; set; }

    public static event EventHandler<NextRaceEventArgs>? NextRaceEvent;

    #endregion

    #region Methods

    /**
     * public static void Initialize(Competition c)
     * Initialize de benodigdheden voor een race
     * Dit zijn Participants (Deelnemers) en Tracks (banen)
     */
    public static Competition Initialize(Competition? c)
    {
        if (c != null) c = new Competition();
        Competition = c;
        AddParticipants(Competition);
        AddTracks(Competition);
        return c!;
    }

    /**
     * public static void StartNextRace()
     * Dit start de volgende race
     */
    public static void StartNextRace(Competition? c)
    {
        CurrentRace?.Clean();
        var nextTrack = c?.GetNextTrack();
        if (nextTrack == null) return;
        if (c?.Participants != null) CurrentRace = new Race(nextTrack, c.Participants);
        if (CurrentRace == null) return;
        CurrentRace.End += OnRaceFinished;
        NextRaceEvent?.Invoke(null, new NextRaceEventArgs
        {
            Race = CurrentRace
        });
        CurrentRace.Start();
    }

    private static void AddParticipants(Competition? c)
    {
        c?.Participants?.Add(new Driver("Mark", 33, new Car(100, 60, 40, false), TeamColors.Blue));
        c?.Participants?.Add(new Driver("Gert", 55, new Car(100, 60, 40, false), TeamColors.Red));
        c?.Participants?.Add(new Driver("Gerwin", 1, new Car(100, 60, 60, false), TeamColors.Green));
        c?.Participants?.Add(new Driver("Rick", 44, new Car(100, 60, 120, false), TeamColors.Yellow));
        c?.Participants?.Add(new Driver("Gerwin", 1, new Car(100, 100, 100, false), TeamColors.Green));
        c?.Participants?.Add(new Driver("Rick", 44, new Car(100, 100, 100, false), TeamColors.Yellow));
        c?.Participants?.Add(new Driver("DR.ROBOT", 100, new Car(1120, 1120, 1120, false), TeamColors.Grey));
    }

    private static void AddTracks(Competition? c)
    {
        c?.Tracks.Enqueue(new Track("Circuit Assen", new[]
        {
            SectionTypes.StartGrid,
            SectionTypes.StartGrid,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.LeftCorner,
            SectionTypes.Straight,
            SectionTypes.RightCorner,
            SectionTypes.Finish,
            SectionTypes.RightCorner,
            SectionTypes.RightCorner,
            SectionTypes.LeftCorner,
            SectionTypes.Straight
        }));


        c?.Tracks.Enqueue(new Track("Circuit Zandvoort", new[]
        {
            SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner,
            SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight,
            SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight,
            SectionTypes.RightCorner, SectionTypes.Finish
        }));
    }

    private static void OnRaceFinished(object? sender, EventArgs args)
    {
        StartNextRace(Competition ?? throw new InvalidOperationException());
    }

    #endregion
}