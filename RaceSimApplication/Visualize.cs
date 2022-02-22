using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace RaceSimApplication
{
    public static class Visualize
    {
        public enum Direction
        {
            North,
            South,
            West,
            East,
        }
        enum Orientation
        {
            Horizontal,
            Vertical,
        }
        public static void Initalize()
        {
        }
        #region Graphics
        private static readonly string[] FinishHorizontal = { "----", " 1# ", "2 # ", "----" };
        private static readonly string[] FinishVertical = { "||||", " 1# ", "2 # ", "||||" };
        private static readonly string[] StartGridHorizontal = { "----", " 1] ", "2]  ", "----" };
        private static readonly string[] StartGridVertical = { "||||", " 1] ", "2]  ", "||||" };


        private static readonly string[] StraightHorizontal = { "----", "  1 ", " 2  ", "----" };
        private static readonly string[] StraightVertical = { "|  |", "|2 |", "| 1|", "|  |" };

        private static readonly string[] CornerNe = { @" /--", @"/1  ", @"| 2 ", @"|  /" };
        private static readonly string[] CornerNw = { @"--\ ", @"  1\", @" 2 |", @"\  |" };
        private static readonly string[] CornerSe = { @"|  \", @"| 1 ", @"\2  ", @" \--" };
        private static readonly string[] CornerSw = { @"/  |", @" 1 |", @"  2/", @"--/ " };
        #endregion
        private static Direction _dir;
        public static Direction direction
        {
            get
            {
                return _dir;
            }
            set
            {
                if (value > Direction.East)
                {
                    _dir = Direction.East;
                }
                if(value < 0)
                {
                    _dir = Direction.North;
                }
            }
        } 

        public static void DrawTrack(Track track)
        {
            direction = Direction.North;
            Orientation orientation = Orientation.Vertical;
            int TrackWidth = 7;

            foreach (Section section in track.Sections)
            {
                DrawSection(direction,section,TrackWidth);
            }
        }
        private static void DrawSection(Direction direction,Section section,int width)
        {
            String[] print = StartGridVertical;
            switch (section.SectionType)
            {
                case SectionTypes.Straight:
                    if(direction == Direction.North || direction == Direction.South)
                    {
                        print = StraightVertical;
                    }
                    else
                    {
                        print = StraightHorizontal;
                    }
                    break;
                case SectionTypes.StartGrid:
                    if (direction == Direction.North || direction == Direction.South)
                    {
                        print = StartGridVertical;
                    }
                    else
                    {
                        print = StartGridHorizontal;
                    }
                    break;
                case SectionTypes.RightCorner:
                    if (direction == Direction.North || direction == Direction.South)
                    {
                        print = CornerNe;
                        direction--;
                    }
                    else
                    {
                        print = CornerSe;
                        direction++;
                    }
                    break;
                case SectionTypes.LeftCorner:
                    if (direction == Direction.North || direction == Direction.South)
                    {
                        print = CornerNw;
                        direction--;
                    }
                    else
                    {
                        print = CornerSw;
                        direction++;
                    }
                    break;
                case SectionTypes.Finish:
                    if (direction == Direction.North || direction == Direction.South)
                    {
                        print = FinishVertical;
                    }
                    else
                    {
                        print = FinishHorizontal;
                    }
                    break;

            }
           
            //Console.SetCursorPosition(7, 2);
            if(direction == Direction.North || direction == Direction.South)
            {
                foreach(String s in print)
                {
                    Console.Write(s);
                }
                Console.WriteLine();
            }
            else
            {
                foreach (String s in print)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
