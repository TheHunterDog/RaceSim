using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Model;

namespace Controller
{
    public class Race
    {
        private System.Timers.Timer timer;
        public Track Track;
        List<IParticipant> Participants;
        DateTime StartTime;
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private Dictionary<IParticipant, int> _wins;
        public int winLimit = 1;
        public event EventHandler<EventArgs> End;

        public int gridsize = 100;
        #region Events
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            MoveParticipants();
            if (checkWin()) { 
                End?.Invoke(this, e); 
            }
            DriversChanged?.Invoke(this, new DriversChangedEventArgs() { track = this.Track });
        }
        public event EventHandler<DriversChangedEventArgs> DriversChanged;
        public void setRandomBroken(Object source, ElapsedEventArgs e)
        {
            Random r = new Random();
            
            int i = r.Next(0,Participants.Count);
            if(Participants[i] != null)
            {
                if (!Participants[i].equipment.isBroken)
                {
                    Participants[i].equipment
                        .isBroken = true;
                }
                else
                {
                    Participants[i].equipment.isBroken = false;
                    Participants[i].equipment.Performance -= r.Next(2, 5);
                    Participants[i].equipment.Quality -= r.Next(2, 8);
                    
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
        }
        public bool checkWin()
        {
            for (int i = 0; i < _wins.Count; i++)
            {
                KeyValuePair<IParticipant, int> w = _wins.ElementAt(i);
                if (w.Value != winLimit+1)
                {
                    return false;
                }
            }
            if(_wins.Count != 0)
            {
                return true;
            }
            return false;
        }
        private bool ParticipantWin(IParticipant p)
        {
            if (_wins.ContainsKey(p))
            {
                if (winLimit != _wins[p])
                {
                    _wins[p] +=1;
                    return false;
                }
                else
                {
                    _wins[p] +=1;
                    return true;
                    p = null;
                }
            }
            else
            {
                _wins[p] = 1;
                return false;
            }
        }
        public void MoveParticipants()
        {

            for(int i = 0; i < _positions.Count; i++)
            {
                KeyValuePair<Section, SectionData> pair = _positions.ElementAt(i);
                KeyValuePair<Section, SectionData> pair2;
                bool finishNext = false;
                if (i == _positions.Count - 1)
                {
                     pair2 = _positions.ElementAt(0);
                }
                else
                {
                    pair2 = _positions.ElementAt(i + 1);
                }
                
                if(pair2.Key.SectionType == SectionTypes.Finish)
                {
                    finishNext = true;
                }
                else { finishNext = false; }
                
                if(pair.Value.Left != null)
                {
                    int speed = 0;
                    if (pair.Value.Left.equipment.isBroken)
                    {
                        speed = 0;
                    }
                    else
                    {
                        speed = pair.Value.Left.equipment.Speed * pair.Value.Left.equipment.Performance;

                        pair.Value.DistanceLeft += speed;
                    }
                    if (pair.Value.DistanceLeft >= gridsize && (pair2.Value.Right == null || pair2.Value.Left == null))
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
                    if (pair.Value.DistacneRight >= gridsize && (pair2.Value.Right == null || pair2.Value.Left == null))
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


        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _positions = new Dictionary<Section, SectionData>();
            _wins = new Dictionary<IParticipant, int>();
            _random = new Random(DateTime.Now.Millisecond);
            placeParticipants();
            timer = new System.Timers.Timer(500);
            timer.Elapsed += OnTimedEvent;
            timer.Elapsed += setRandomBroken;
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
            foreach (Driver participant in Participants)
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
}
