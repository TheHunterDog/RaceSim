using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using WPF;
using Model;
using RaceSimApplication;

namespace WPF
{
    public static class Visualize
    {
        public static VisualizeR.Direction CurDir = VisualizeR.Direction.E;
        private static int posX = 350;
        private static int posY = 200;

        private static Controller.Race _Race;
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

        #endregion

        public static void Init(Controller.Race r)
        {
            _Race = r;
        }
        public static BitmapSource DrawTrack(Track t)
        {
            Bitmap b = Images.GetEmptyBitmap(800, 800);
            Graphics g = Graphics.FromImage(b);

            foreach (Section s in t.Sections)
            {
                Bitmap section = Images.LoadImage(getSectionURL(s.SectionType, CurDir));
                g.DrawImage(section, posX, posY, 100, 100);
                changeDirection(s.SectionType);

                UpdateCursor();
            }
            return Images.CreateBitmapSourceFromGdiBitmap(b);
        }

        // Visualize the Participants on the Track
        public static void VisualizeParticipants(Section section, Graphics g)
        {
            // Fetch the Participants
            IParticipant leftParticipant = _Race.GetSectionData(section).Left;
            IParticipant rightParticipant = _Race.GetSectionData(section).Right;

            if (leftParticipant != null)
            {
                DrawParticipant(leftParticipant, g, section, Controller.Side.Left);
            }

            if (rightParticipant != null)
            {
                DrawParticipant(rightParticipant, g, section, Controller.Side.Right);
            }
        }

        // Draw the participant on Track piece
        public static void DrawParticipant(IParticipant participant, Graphics g, Section section, Controller.Side side)
        {
            (int x, int y) = CalculateParticipantPos(section.SectionType, side);
            Bitmap participantBitmap = Images.LoadImage(FetchParticipantBitmapUrl(participant));
            g.DrawImage(participantBitmap, posX + x, posY + y);
        }

        // Calculate the position for the drivers
        public static (int x, int y) CalculateParticipantPos(SectionTypes sectionType, Controller.Side side)
        {
            return CurDir switch
            {
                VisualizeR.Direction.N => side switch
                {
                    Controller.Side.Left => (20, 20),
                    Controller.Side.Right => (50, 20)
                },
                VisualizeR.Direction.E => side switch
                {
                    Controller.Side.Left => (20, 20),
                    Controller.Side.Right => (20, 50)
                },
                VisualizeR.Direction.S => side switch
                {
                    Controller.Side.Left => (50, 20),
                    Controller.Side.Right => (20, 20)
                },
                VisualizeR.Direction.W => side switch
                {
                    Controller.Side.Left => (20, 50),
                    Controller.Side.Right => (20, 20)
                }
            };
        }

        public static string FetchParticipantBitmapUrl(IParticipant participant)
        {
            return CurDir switch
            {
                VisualizeR.Direction.N => participant.TeamColors switch
                {
                    TeamColors.Blue => _driverBlueUp,
                    TeamColors.Green => _driverGreenUp,
                    TeamColors.Grey => _driverGreyUp,
                    TeamColors.Red => _driverRedUp,
                    TeamColors.Yellow => _driverYellowUp
                },
                VisualizeR.Direction.E => participant.TeamColors switch
                {
                    TeamColors.Blue => _driverBlueRight,
                    TeamColors.Green => _driverGreenRight,
                    TeamColors.Grey => _driverGreyRight,
                    TeamColors.Red => _driverRedRight,
                    TeamColors.Yellow => _driverYellowRight
                },
                VisualizeR.Direction.S => participant.TeamColors switch
                {
                    TeamColors.Blue => _driverBlueDown,
                    TeamColors.Green => _driverGreenDown,
                    TeamColors.Grey => _driverGreyDown,
                    TeamColors.Red => _driverRedDown,
                    TeamColors.Yellow => _driverYellowDown
                },
                VisualizeR.Direction.W => participant.TeamColors switch
                {
                    TeamColors.Blue => _driverBlueLeft,
                    TeamColors.Green => _driverGreenLeft,
                    TeamColors.Grey => _driverGreyLeft,
                    TeamColors.Red => _driverRedLeft,
                    TeamColors.Yellow => _driverYellowLeft
                }
            };
        }




        private static void UpdateCursor()
        {
            int size = 100;

            switch (CurDir)
            {
                case VisualizeR.Direction.N:
                    posY -= size;
                    break;

                case VisualizeR.Direction.E:
                    posX += size;
                    break;

                case VisualizeR.Direction.S:
                    posY += size;
                    break;

                case VisualizeR.Direction.W:
                    posX -= size;
                    break;
            }
        }
        public static void changeDirection(SectionTypes type)
        {
            if (type == SectionTypes.RightCorner)
            {
                switch (CurDir)
                {
                    case VisualizeR.Direction.N:
                        CurDir = VisualizeR.Direction.E;
                        break;
                    case VisualizeR.Direction.E:
                        CurDir = VisualizeR.Direction.S;
                        break;
                    case VisualizeR.Direction.S:
                        CurDir = VisualizeR.Direction.W;
                        break;
                    case VisualizeR.Direction.W:
                        CurDir = VisualizeR.Direction.N;
                        break;
                }
            }
            else if (type == SectionTypes.LeftCorner)
            {
                switch (CurDir)
                {
                    case VisualizeR.Direction.N:
                        CurDir = VisualizeR.Direction.W;
                        break;
                    case VisualizeR.Direction.E:
                        CurDir = VisualizeR.Direction.N;
                        break;
                    case VisualizeR.Direction.S:
                        CurDir = VisualizeR.Direction.E;
                        break;
                    case VisualizeR.Direction.W:
                        CurDir = VisualizeR.Direction.S;
                        break;
                }
            }
        }
        private static String getSectionURL(SectionTypes ST, RaceSimApplication.VisualizeR.Direction dir)
        {
            return ST switch
            {
                SectionTypes.StartGrid => _startGrid,
                SectionTypes.Finish => dir switch
                {
                    VisualizeR.Direction.N => _finishNorth,
                    VisualizeR.Direction.E => _finishEast,
                    VisualizeR.Direction.S => _finishSouth,
                    VisualizeR.Direction.W => _finishWest,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.Straight => dir switch
                {
                    VisualizeR.Direction.N => _straightVertical,
                    VisualizeR.Direction.E => _straightHorizontal,
                    VisualizeR.Direction.S => _straightVertical,
                    VisualizeR.Direction.W => _straightHorizontal,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.RightCorner => dir switch
                {
                    VisualizeR.Direction.N => _cornerLeftTop,
                    VisualizeR.Direction.E => _cornerRightTop,
                    VisualizeR.Direction.S => _cornerRightBottom,
                    VisualizeR.Direction.W => _cornerLeftBottom,
                    _ => throw new NotImplementedException()
                },
                SectionTypes.LeftCorner => dir switch
                {
                    VisualizeR.Direction.N => _cornerRightTop,
                    VisualizeR.Direction.E => _cornerRightBottom,
                    VisualizeR.Direction.S => _cornerLeftBottom,
                    VisualizeR.Direction.W => _cornerLeftTop,
                    _ => throw new NotImplementedException()
                },
                _ => throw new NotImplementedException()
            };
        }
    }
}
