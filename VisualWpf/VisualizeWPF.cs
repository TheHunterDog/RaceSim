using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media.Imaging;
using Model;
using Controller;
using System.Diagnostics;

namespace WPF
{
    public static class VisualizeWPF
    {
        private static int _posX = 350;
        private static int _posY = 200;
        public static Direction _currentDirection = Direction.E;

        private static Race _currentRace;

        #region graphics

        // Track pieces
        private const string _startGrid = @".\assets\sections\track_section_startgrid.png";

        private const string _finishNorth = @".\assets\sections\track_section_finish_vertical.png";
        private const string _finishEast = @".\assets\sections\track_section_finish_horizontal.png";
        private const string _finishSouth = @".\assets\sections\track_section_finish_vertical.png";
        private const string _finishWest = @".\assets\sections\track_section_finish_horizontal.png";

        private const string _straightHorizontal = @".\assets\sections\track_section_straight_horizontal.png";
        private const string _straightVertical = @".\assets\sections\track_section_straight_vertical.png";

        private const string _cornerLeftTop = @".\assets\sections\track_section_corner_topleft.png";
        private const string _cornerLeftBottom = @".\assets\sections\track_section_corner_bottomleft.png";
        private const string _cornerRightTop = @".\assets\sections\track_section_corner_topright.png";
        private const string _cornerRightBottom = @".\assets\sections\track_section_corner_bottomright.png";

        // Drivers
        private const string _driverBlueUp = @".\assets\drivers\driver_blue_vertical_top.png";
        private const string _driverBlueDown = @".\assets\drivers\driver_blue_vertical_down.png";
        private const string _driverBlueRight = @".\assets\drivers\driver_blue_horizontal_right.png";
        private const string _driverBlueLeft = @".\assets\drivers\driver_blue_horizontal_left.png";

        private const string _driverGreenUp = @".\assets\drivers\driver_green_vertical_top.png";
        private const string _driverGreenDown = @".\assets\drivers\driver_green_vertical_down.png";
        private const string _driverGreenRight = @".\assets\drivers\driver_green_horizontal_right.png";
        private const string _driverGreenLeft = @".\assets\drivers\driver_green_horizontal_left.png";

        private const string _driverGreyUp = @".\assets\drivers\driver_grey_vertical_top.png";
        private const string _driverGreyDown = @".\assets\drivers\driver_grey_vertical_down.png";
        private const string _driverGreyRight = @".\assets\drivers\driver_grey_horizontal_right.png";
        private const string _driverGreyLeft = @".\assets\drivers\driver_grey_horizontal_left.png";

        private const string _driverRedUp = @".\assets\drivers\driver_red_vertical_top.png";
        private const string _driverRedDown = @".\assets\drivers\driver_red_vertical_down.png";
        private const string _driverRedRight = @".\assets\drivers\driver_red_horizontal_right.png";
        private const string _driverRedLeft = @".\assets\drivers\driver_red_horizontal_left.png";

        private const string _driverYellowUp = @".\assets\drivers\driver_yellow_vertical_top.png";
        private const string _driverYellowDown = @".\assets\drivers\driver_yellow_vertical_down.png";
        private const string _driverYellowRight = @".\assets\drivers\driver_yellow_horizontal_right.png";
        private const string _driverYellowLeft = @".\assets\drivers\driver_yellow_horizontal_left.png";
        // Broken
        private const string _broken = @".\assets\Attributes\BrokenFire.png";
        #endregion

        public static void Initialize(Race race)
        {
            _currentRace = race;
        }

        // Draw the track by drawing every section
        public static BitmapSource DrawTrack(Track track)
        {
            // Create track canvas
            Bitmap Canvas = Images.GetEmptyBitmap(800, 800);
            Graphics g = Graphics.FromImage(Canvas);

            // Draw every section and the participants
            foreach (Section section in track.Sections)
            {
                Bitmap sectionBitmap = Images.LoadImage(GetSectionVisual(section.SectionType, _currentDirection));
                g.DrawImage(sectionBitmap, _posX, _posY, 100, 100);

                VisualizeParticipants(section, g);

                UpdateDirection(section.SectionType);
                UpdateCursor();
            }

            return Images.CreateBitmapSourceFromGdiBitmap(Canvas);
        }

        // Visualize the participants on the track
        public static void VisualizeParticipants(Section section, Graphics g)
        {
            // Fetch the participants
            IParticipant leftParticipant = _currentRace.GetSectionData(section).Left;
            IParticipant rightParticipant = _currentRace.GetSectionData(section).Right;

            if (leftParticipant != null)
            {
                DrawParticipant(leftParticipant, g, section, Side.Left);
            }

            if (rightParticipant != null)
            {
                DrawParticipant(rightParticipant, g, section, Side.Right);
            }
        }

        // Draw the participant on track piece
        public static void DrawParticipant(IParticipant participant, Graphics g, Section section, Side side)
        {
            (int x, int y) = CalculateParticipantPos(section.SectionType, side);
            Bitmap participantBitmap = Images.LoadImage(FetchParticipantBitmapUrl(participant));
            g.DrawImage(participantBitmap, _posX+x, _posY+y);
            if (participant.equipment.isBroken)
            {
                Bitmap broken = Images.LoadImage(_broken);
                g.DrawImage(broken, (_posX), _posY);
            }
        }

        // Calculate the position for the drivers
        public static (int x, int y) CalculateParticipantPos(SectionTypes sectionType, Side side)
        {
            return _currentDirection switch
            {
                Direction.N => side switch
                {
                    Side.Left => (20, 20),
                    Side.Right => (50, 20)
                },
                Direction.E => side switch
                {
                    Side.Left => (20, 20),
                    Side.Right => (20, 50)
                },
                Direction.S => side switch
                {
                    Side.Left => (50, 20),
                    Side.Right => (20, 20)
                },
                Direction.W => side switch
                {
                    Side.Left => (20, 50),
                    Side.Right => (20, 20)
                }
            };
        }

        public static string FetchParticipantBitmapUrl(IParticipant participant)
        {
            return _currentDirection switch
            {
                Direction.N => participant.TeamColors switch
                {
                    TeamColors.Blue => _driverBlueUp,
                    TeamColors.Green => _driverGreenUp,
                    TeamColors.Grey => _driverGreyUp,
                    TeamColors.Red => _driverRedUp,
                    TeamColors.Yellow => _driverYellowUp
                },
                Direction.E => participant.TeamColors switch
                {
                    TeamColors.Blue => _driverBlueRight,
                    TeamColors.Green => _driverGreenRight,
                    TeamColors.Grey => _driverGreyRight,
                    TeamColors.Red => _driverRedRight,
                    TeamColors.Yellow => _driverYellowRight
                },
                Direction.S => participant.TeamColors switch
                {
                    TeamColors.Blue => _driverBlueDown,
                    TeamColors.Green => _driverGreenDown,
                    TeamColors.Grey => _driverGreyDown,
                    TeamColors.Red => _driverRedDown,
                    TeamColors.Yellow => _driverYellowDown
                },
                Direction.W => participant.TeamColors switch
                {
                    TeamColors.Blue => _driverBlueLeft,
                    TeamColors.Green => _driverGreenLeft,
                    TeamColors.Grey => _driverGreyLeft,
                    TeamColors.Red => _driverRedLeft,
                    TeamColors.Yellow => _driverYellowLeft
                }
            };
        }

        private static string GetSectionVisual(SectionTypes sectionType, Direction direction)
        {
            return sectionType switch
            {
                SectionTypes.StartGrid => _startGrid,
                SectionTypes.Finish => direction switch
                {
                    Direction.N => _finishNorth,
                    Direction.E => _finishEast,
                    Direction.S => _finishSouth,
                    Direction.W => _finishWest,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.Straight => direction switch
                {
                    Direction.N => _straightVertical,
                    Direction.E => _straightHorizontal,
                    Direction.S => _straightVertical,
                    Direction.W => _straightHorizontal,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.RightCorner => direction switch
                {
                    Direction.N => _cornerLeftTop,
                    Direction.E => _cornerRightTop,
                    Direction.S => _cornerRightBottom,
                    Direction.W => _cornerLeftBottom,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.LeftCorner => direction switch
                {
                    Direction.N => _cornerRightTop,
                    Direction.E => _cornerRightBottom,
                    Direction.S => _cornerLeftBottom,
                    Direction.W => _cornerLeftTop,
                    _ => throw new NotImplementedException()
                },
                _ => throw new NotImplementedException()
            };
        }

        // Update the direction if corner is printed
        private static void UpdateDirection(SectionTypes type)
        {
            if (type == SectionTypes.RightCorner)
            {
                switch (_currentDirection)
                {
                    case Direction.N:
                        _currentDirection = Direction.E;
                        break;
                    case Direction.E:
                        _currentDirection = Direction.S;
                        break;
                    case Direction.S:
                        _currentDirection = Direction.W;
                        break;
                    case Direction.W:
                        _currentDirection = Direction.N;
                        break;
                }
            }
            else if (type == SectionTypes.LeftCorner)
            {
                switch (_currentDirection)
                {
                    case Direction.N:
                        _currentDirection = Direction.W;
                        break;
                    case Direction.E:
                        _currentDirection = Direction.N;
                        break;
                    case Direction.S:
                        _currentDirection = Direction.E;
                        break;
                    case Direction.W:
                        _currentDirection = Direction.S;
                        break;
                }
            }
        }

        private static void UpdateCursor()
        {
            int size = 100;

            switch (_currentDirection)
            {
                case Direction.N:
                    _posY -= size;
                    break;

                case Direction.E:
                    _posX += size;
                    break;

                case Direction.S:
                    _posY += size;
                    break;

                case Direction.W:
                    _posX -= size;
                    break;
            }
        }
    }

    // Define the track directions
    public enum Direction
    {
        N,
        E,
        S,
        W
    }
}
