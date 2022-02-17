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
            _random = new Random(DateTime.Now.Millisecond);

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

        SectionData GetSectionData(Section section) { 
          SectionData  SectionData = _positions[section]; 
            if(SectionData == null)
            {
               return _positions[section] = new SectionData();
            }
            return SectionData;
        }
    }
}
