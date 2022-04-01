using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SectionData
    {
        public IParticipant? Left { get; set; }
        public IParticipant? Right { get; set; }
        public int DistanceLeft { get; set; }
        public int DistacneRight { get; set; }
        public SectionData()
        {
        }
        public bool addParicpantTpSection(IParticipant? p )
        {
            if(Right == null)
            {
                Right = p;
                return true;
            }
            else if(Left == null)
            {
                Left = p;
                return true;
            }
            return false;
        }
    }
}
