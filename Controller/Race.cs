using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
     public class Race
    {
        public Track Track;
        List<IParticipant> Participants;
        DateTime StartTime;
        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _positions = new Dictionary<Section, SectionData>();
            _random = new Random(DateTime.Now.Millisecond);
            placeParticipants();

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
