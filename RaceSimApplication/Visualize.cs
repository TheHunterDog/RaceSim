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
        private static Track _curTrack;
        private static Direction _dir;
        private static int _posX = Console.WindowWidth / 2;
        private static int _posY = Console.WindowWidth / 2;
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
                if (value < 0)
                {
                    _dir = Direction.North;
                }
            }
        }

        public static void Initalize(Track track)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            direction = Direction.East;
            _curTrack = track;
            verifyTrack(track);
            DrawTrack();

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

        private static bool verifyTrack(Track track)
        {
            int x = 0, y = 0;
            Direction dir = Direction.North;


            foreach (var section in track.Sections)
            {
                switch (section.SectionType)
                {
                    case SectionTypes.LeftCorner:
                        dir++;
                        break;
                    case SectionTypes.RightCorner:
                        dir--;
                        break;

                }
                if (dir == Direction.North)
                {
                    y--;
                }
                else if (dir == Direction.East)
                {
                    x++;
                }
                else if(dir == Direction.West)
                {
                    x--;
                }
                else if(dir == Direction.South)
                {
                    y++;
                }
            }

            return x == 0 && y == 0;
        }
        #region defa
        /*        private static void DrawSection(Direction direction, LinkedList<Section> sections, int width)
                {
                    Direction tempDir = direction;
                    String[] print = StartGridVertical;
                    foreach (Section section in sections)
                    {
                        switch (section.SectionType)
                        {
                            case SectionTypes.Straight:
                                if (direction == Direction.North || direction == Direction.South)
                                {
                                    print = StraightVertical;
                                    maxHight++;
                                }
                                else
                                {
                                    print = StraightHorizontal;
                                    maxWidth++;
                                }
                                break;
                            case SectionTypes.StartGrid:
                                if (direction == Direction.North || direction == Direction.South)
                                {
                                    print = StartGridVertical;
                                    maxHight++;
                                }
                                else
                                {
                                    print = StartGridHorizontal;
                                    maxWidth++;
                                }
                                break;
                            case SectionTypes.RightCorner:
                                if (direction == Direction.North || direction == Direction.South)
                                {
                                    print = CornerNe;
                                    tempDir++;
                                }
                                else
                                {
                                    print = CornerSe;
                                    tempDir--;
                                }
                                break;
                            case SectionTypes.LeftCorner:
                                if (direction == Direction.North || direction == Direction.South)
                                {
                                    print = CornerNw;
                                    tempDir++;
                                }
                                else
                                {
                                    print = CornerSw;
                                    tempDir--;

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

                        Console.SetCursorPosition(x, y);
                        if (direction == Direction.North || direction == Direction.South)
                        {
                            foreach (String s in print)
                            {
                                Console.Write(s);
                            }
                            Console.WriteLine();
                            y++;
                        }
                        else
                        {
                            foreach (String s in print)
                            {
                                Console.SetCursorPosition(x, y);
                                Console.WriteLine(s);
                                y++;
                            }
                        }

                        if (direction == Direction.North)
                        {
                            y -= 4;
                        }
                        if (direction == Direction.South)
                        {
                            y += 4;

                        }
                        if (direction == Direction.West)
                        {
                            x -= 4;
                        }
                        if (direction == Direction.East)
                        {
                            x += 4;
                        }

                        direction = tempDir;
                    }
                }
            */
        #endregion

        private static void DrawTrack()
        {
            _posX = 50;
            _posY = 10;
            Console.SetCursorPosition(_posX, _posY);

            foreach (Section section in _curTrack.Sections)
            {
                drawSection(section);
            }
        }

        private static void drawSection(Section section)
        {
            String[] sectionVisual = getSectionVisual(section.SectionType, direction);

            int cury = _posY;
            foreach(String s  in sectionVisual)
            {
                Console.SetCursorPosition(_posX, cury);
                Console.WriteLine(s);
                cury++;
            }
            UpdateDirection(section.SectionType);
            UpdateCursor();
        }

        private static void UpdateDirection(SectionTypes sectionType)
        {
            if(sectionType == SectionTypes.RightCorner)
            {
                switch (direction)
                {
                    case Direction.North:
                        direction = Direction.East;
                        break;
                    case Direction.East:
                        direction = Direction.South;
                        break;
                    case Direction.South:
                        direction = Direction.West;
                        break;
                    case Direction.West:
                        direction = Direction.North;
                        break;
                }
            }
            else if(sectionType == SectionTypes.LeftCorner)
            {
                switch (direction)
                {
                    case Direction.North:
                        direction = Direction.West;
                        break;
                    case Direction.East:
                        direction= Direction.North;
                        break;
                    case Direction.South:
                        direction = Direction.East;
                        break;
                    case Direction.West :
                        direction = Direction.South;
                        break;
                }
            }
        }

        private static void UpdateCursor()
        {
            int size = 4;

            switch (direction)
            {
                case Direction.North:
                    _posY -= size;
                    break;
                case Direction.East :
                    _posX += size;
                    break;
                case Direction.South :
                    _posY += size;
                    break;
                case Direction.West:
                    _posX -= size;
                    break;
            }
        }

        private static string[] getSectionVisual(SectionTypes sectionType, Direction dir)
        {
            return sectionType switch
            {
                SectionTypes.StartGrid => StartGridVertical,
                SectionTypes.Finish => dir switch
                {
                    Direction.North => FinishVertical,
                    Direction.East => FinishVertical,
                    Direction.South => FinishVertical,
                    Direction.West => FinishHorizontal,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.Straight => dir switch
                {
                    Direction.North => StraightVertical,
                    Direction.East => StraightHorizontal,
                    Direction.South => StraightVertical,
                    Direction.West => StraightHorizontal,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.RightCorner => dir switch
                {
                    Direction.North => CornerNw,
                    Direction.East => CornerNe,
                    Direction.South => CornerSe,
                    Direction.West => CornerSw,
                    _ => throw new NotImplementedException()

                },
                SectionTypes.LeftCorner => dir switch
                {
                    Direction.North => CornerNe,
                    Direction.East => CornerNw,
                    Direction.South => CornerSw,
                    Direction.West => CornerSe,
                    _ => throw new NotImplementedException(),

                },
                _ => throw new NotImplementedException()
            };
        }
    }
}
