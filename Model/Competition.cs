using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> participants { get; set; }
        public Queue<Track> tracks { get; set; }
        public Competition()
        {
            participants = new List<IParticipant>();
            tracks = new Queue<Track>();
        }
        public Track NextTrack()
        {
            return tracks.Count > 0 ? tracks.Dequeue() : null;
        }
    }
}
    