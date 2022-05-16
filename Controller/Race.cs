using System.Timers;
using Model;
using Timer = System.Timers.Timer;

namespace Controller;

public class Race
{
    private const int GridSize = 100;

    // private readonly Random _random;
    private readonly Dictionary<Section, SectionData?> _positions;
    private readonly Timer _timer;

    private readonly int _winLimit = 3;


    public Race(Track? track, List<IParticipant?>? participants)
    {
        Track = track;
        Participants = participants;
        _positions = new Dictionary<Section, SectionData?>();
        Wins = new Dictionary<IParticipant, int>();
        // var _random = new Random(DateTime.Now.Millisecond);
        PlaceParticipants();
        _timer = new Timer(500);
        _timer.Elapsed += OnTimedEvent;
        _timer.Elapsed += SetRandomBroken;
        Start();
    }

    public Track? Track { get; }
    public List<IParticipant?>? Participants { get; }

    private Dictionary<IParticipant, int> Wins { get; }
    
    public event EventHandler<EventArgs>? End;

    public void Start()
    {
        _timer.Start();
    }

    public void Clean()
    {
        DriversChanged = null;
        _timer.Stop();
        End = null;
        Wins.Clear();
    }

    //check if all Participants have reached winLimit
    private bool CheckWin(Dictionary<IParticipant, int> winners)
    {
        for (var i = 0; i < Wins.Count; i++)
        {
            var w = winners.ElementAt(i);
            if (w.Value != _winLimit) return false;
        }

        if (Participants != null && (winners.Count == 0 || winners.Count != Participants.Count)) return false;
        Wins.Clear();
        return true;
    }

    private bool ParticipantWin(IParticipant? p)
    {
        if (p != null)
        {
            if (Wins.ContainsKey(p))
            {
                if (_winLimit != Wins[p])
                {
                    Wins[p] += 1;
                    return false;
                }

                Wins[p] += 1;
                return true;
                //p = null;
            }

            Wins.Add(p, 1);
            // _wins[p] = 1;
            return false;
        }

        return false;
    }


    private void MoveParticipants()
    {
        for (var i = 0; i < _positions.Count; i++)
        {
            var pair = _positions.ElementAt(i);
            var pair2 = i == _positions.Count - 1 ? _positions.ElementAt(0) : _positions.ElementAt(i + 1);

            var finishNext = pair2.Key.SectionType == SectionTypes.Finish;

            if (pair.Value?.Left != null)
            {
                int speed;
                if (pair.Value.Left.Equipment.IsBroken)
                {
                    speed = 0;
                }
                else
                {
                    speed = pair.Value.Left.Equipment.Speed * pair.Value.Left.Equipment.Performance;

                    pair.Value.DistanceLeft += speed;
                }

                if (pair.Value.DistanceLeft >= GridSize && (pair2.Value?.Right == null || pair2.Value.Left == null))
                {
                    if (pair2.Value?.Right == null)
                    {
                        if (pair2.Value != null)
                        {
                            pair2.Value.Right = pair.Value.Left;
                            if (finishNext)
                                if (ParticipantWin(pair2.Value.Right))
                                    pair2.Value.Right = null;
                        }
                    }
                    else if (pair2.Value.Left == null)
                    {
                        pair2.Value.Left = pair.Value.Left;
                        if (finishNext)
                            if (ParticipantWin(pair2.Value.Left))
                                pair2.Value.Left = null;
                    }

                    pair.Value.Left = null;
                    pair.Value.DistanceLeft = 0;
                }
                else
                {
                    pair.Value.DistanceLeft += speed;
                }
            }

            if (pair.Value?.Right != null)
            {
                int speed;
                if (pair.Value.Right.Equipment.IsBroken)
                    speed = 0;
                else
                    speed = pair.Value.Right.Equipment.Speed * pair.Value.Right.Equipment.Performance;
                if (pair.Value.DistacneRight >= GridSize && (pair2.Value?.Right == null || pair2.Value.Left == null))
                {
                    if (pair2.Value?.Right == null)
                    {
                        if (pair2.Value != null)
                        {
                            pair2.Value.Right = pair.Value.Right;
                            if (finishNext)
                                if (ParticipantWin(pair2.Value.Right))
                                    pair2.Value.Right = null;
                        }
                    }
                    else if (pair2.Value.Left == null)
                    {
                        pair2.Value.Left = pair.Value.Right;
                        if (finishNext)
                            if (ParticipantWin(pair2.Value.Left))
                                pair2.Value.Left = null;
                    }

                    pair.Value.Right = null;
                    pair.Value.DistacneRight = 0;
                }
                else
                {
                    pair.Value.DistacneRight += speed;
                }
            }
        }
    }

    private void PlaceParticipants()
    {
        if (Participants == null) return;
        foreach (var t in Participants)
        foreach (var j in Track?.Sections!)
        {
            var s = GetSectionData(j);
            if (s != null && s.AddParticipantToSection(t)) break;
        }
    }

    public SectionData? GetSectionData(Section section)
    {
        if (!_positions.ContainsKey(section)) _positions.Add(section, new SectionData());
        var sectionData = _positions[section];
        return sectionData;
    }

    #region Events

    private void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        MoveParticipants();
        DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));

        if (CheckWin(Wins)) End?.Invoke(this, e);
    }

    public event EventHandler<DriversChangedEventArgs>? DriversChanged;

    private void SetRandomBroken(object? source, ElapsedEventArgs e)
    {
        var r = new Random();

        if (Participants == null) return;
        var i = r.Next(1, Participants.Count);
        if (Participants.Count < i || i < 1) return;
        i -= 1;
        if (Participants[i] == null) return;
        if (!Participants[i]!.Equipment.IsBroken)
        {
            Participants[i]!.Equipment
                .IsBroken = true;
        }
        else
        {
            Participants[i]!.Equipment.IsBroken = false;
            Participants[i]!.Equipment.Performance -= r.Next(0, 3);
            Participants[i]!.Equipment.Quality -= r.Next(0, 4);
        }
    }

    #endregion
}

public enum Side
{
    Left,
    Right
}