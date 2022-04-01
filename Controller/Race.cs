using System.Timers;
using Model;

namespace Controller
{
    public class Race
    {
        private System.Timers.Timer timer;
        public Track Track { get; }
        public List<IParticipant?> Participants { get; }
        private readonly Random _random;
        private readonly Dictionary<Section, SectionData> _positions;
        private Dictionary<IParticipant, int> Wins { get; }

        private int winLimit = 3;
        //todo Set to property
        public event EventHandler<EventArgs>? End;

        private readonly int _gridsize = 100;
        #region Events
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            MoveParticipants();
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(this.Track));

            if (CheckWin(Wins)) { 
                End?.Invoke(this, e); 
            }
        }
        public event EventHandler<DriversChangedEventArgs>? DriversChanged;

        private void SetRandomBroken(Object source, ElapsedEventArgs e)
        {
            var r = new Random();
            
            var i = r.Next(1,Participants.Count);
            if(Participants.Count >= i && i >= 1)
            {
                i -= 1;
                if (Participants[i] != null)
                {
                    if (!Participants[i]!.equipment.isBroken)
                    {
                        Participants[i]!.equipment
                            .isBroken = true;
                    }
                    else
                    {
                        Participants[i]!.equipment.isBroken = false;
                        Participants[i]!.equipment.Performance -= r.Next(0, 3);
                        Participants[i]!.equipment.Quality -= r.Next(0, 4);

                    }
                }
            }


            
        }
        #endregion
        public void Start()
        {
            timer.Start();
        }

        public void Clean()
        {
            DriversChanged = null;
            timer.Stop();
            End = null;
            Wins.Clear();
        }
        //check if all Participants have reached winLimit
        private bool CheckWin(Dictionary<IParticipant, int> winners)
        {
            for (var i = 0; i < Wins.Count; i++)
            {
                var w = winners.ElementAt(i);
                if (w.Value != winLimit)
                {
                    return false;
                }
            }
            if(winners.Count != 0 && winners.Count == this.Participants.Count)
            {
                Wins.Clear();
                return true;
            }
            return false;
        }
        private bool ParticipantWin(IParticipant? p)
        {
            if (Wins.ContainsKey(p))
            {
                if (winLimit != Wins[p])
                {
                    Wins[p] += 1;
                    return false;
                }
                else
                {
                    Wins[p] += 1;
                    return true;
                    //p = null;
                }
            }
            else
            { 
                Wins.Add(p, 1);
               // _wins[p] = 1;
                return false;
            }
        }
        public void MoveParticipants()
        {

            for(int i = 0; i < _positions.Count; i++)
            {
                var pair = _positions.ElementAt(i);
                bool finishNext;
                var pair2 = i == _positions.Count - 1 ? _positions.ElementAt(0) : _positions.ElementAt(i + 1);
                
                if(pair2.Key.SectionType == SectionTypes.Finish)
                {
                    finishNext = true;
                }
                else { finishNext = false; }
                
                if(pair.Value.Left != null)
                {
                    var speed = 0;
                    if (pair.Value.Left.equipment.isBroken)
                    {
                        speed = 0;
                    }
                    else
                    {
                        speed = pair.Value.Left.equipment.Speed * pair.Value.Left.equipment.Performance;

                        pair.Value.DistanceLeft += speed;
                    }
                    if (pair.Value.DistanceLeft >= _gridsize && (pair2.Value.Right == null || pair2.Value.Left == null))
                    {
                        if (pair2.Value.Right == null)
                        {
                            pair2.Value.Right = pair.Value.Left;
                            if (finishNext)
                            {
                                if (ParticipantWin(pair2.Value.Right))
                                {
                                    pair2.Value.Right = null;

                                }
                            }
                            
                            
                        }
                        else if (pair2.Value.Left == null)
                        {
                            pair2.Value.Left = pair.Value.Left;
                            if (finishNext)
                            {
                                if(ParticipantWin(pair2.Value.Left)){
                                    pair2.Value.Left = null;
                                }
                            }
                            
                        }
                       
                        pair.Value.Left = null;
                        pair.Value.DistanceLeft = 0;
                        
                        
                    }
                    else
                    {
                        pair.Value.DistanceLeft += speed;
                    }
                }
                if (pair.Value.Right != null)
                {
                    int speed;
                    if (pair.Value.Right.equipment.isBroken)
                    {
                        speed = 0;
                    }
                    else
                    {
                        speed = pair.Value.Right.equipment.Speed * pair.Value.Right.equipment.Performance;
                    }
                    if (pair.Value.DistacneRight >= _gridsize && (pair2.Value.Right == null || pair2.Value.Left == null))
                    {
                        if (pair2.Value.Right == null)
                        {
                            pair2.Value.Right = pair.Value.Right;
                            if (finishNext)
                            {
                                if (ParticipantWin(pair2.Value.Right))
                                {
                                    pair2.Value.Right = null;
                                }
                            }
                        }
                        else if (pair2.Value.Left == null)
                        {
                            pair2.Value.Left = pair.Value.Right;
                            if (finishNext)
                            {
                                if (ParticipantWin(pair2.Value.Left))
                                {
                                    pair2.Value.Left = null;
                                }
                            }
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


        public Race(Track track, List<IParticipant?> participants)
        {
            Track = track;
            Participants = participants;
            _positions = new Dictionary<Section, SectionData>();
            Wins = new Dictionary<IParticipant?, int>();
            _random = new Random(DateTime.Now.Millisecond);
            placeParticipants();
            timer = new System.Timers.Timer(500);
            timer.Elapsed += OnTimedEvent;
            timer.Elapsed += SetRandomBroken;
            Start();
        }
        void placeParticipants()
        {
            for (int i = 0; i < Participants.Count; i++)
            {
                for(int j = 0; j < Track.Sections.Count; j++)
                {
                    SectionData s = GetSectionData(Track.Sections.ElementAt(j));
                    if (s.addParicpantTpSection(Participants[i])){
                        break;
                    }
                }
            }
        }

        void RandommizeEquipment()
        {
            foreach (Driver? participant in Participants)
            {
                participant.equipment.Performance = _random.Next();
                participant.equipment.Speed = _random.Next();
                participant.equipment.Quality = _random.Next();
            }
        }

        public SectionData GetSectionData(Section section) {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData()); 
            }
          SectionData  SectionData = _positions[section];
                
            if(SectionData == null)
            {
               return _positions[section] = new SectionData();
            }
            return SectionData;
        }
    }
    public enum Side
    {
        Left,
        Right,
    }
}
