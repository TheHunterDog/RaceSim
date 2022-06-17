using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using Controller;
using Model;

namespace WPF;

public static class VisualizeWpf
{
    private static int _posX = 350;
    private static int _posY = 200;
    private static Direction _currentDirection = Direction.E;

    private static Race? _currentRace;

    public static void Initialize(Race? race)
    {
        _currentRace = race;
    }

    // Draw the Track by drawing every section
    public static BitmapSource DrawTrack(Track? track)
    {
        // Create Track canvas
        var canvas = Images.GetEmptyBitmap(800, 800);
        var g = Graphics.FromImage(canvas);

        // Draw every section and the Participants
        if (track?.Sections != null)
            foreach (var section in track.Sections)
            {
                var sectionBitmap = Images.LoadImage(GetSectionVisual(section.SectionType, _currentDirection));
                g.DrawImage(sectionBitmap, _posX, _posY, 100, 100);

                VisualizeParticipants(section, g);

                UpdateDirection(section.SectionType);
                UpdateCursor();
            }

        return Images.CreateBitmapSourceFromGdiBitmap(canvas);
    }

    // Visualize the Participants on the Track
    private static void VisualizeParticipants(Section section, Graphics g)
    {
        // Fetch the Participants
        var leftParticipant = _currentRace?.GetSectionData(section)?.Left;
        var rightParticipant = _currentRace?.GetSectionData(section)?.Right;

        if (leftParticipant != null) DrawParticipant(leftParticipant, g, Side.Left);

        if (rightParticipant != null) DrawParticipant(rightParticipant, g, Side.Right);
    }

    // Draw the participant on Track piece
    private static void DrawParticipant(IParticipant? participant, Graphics g, Side side)
    {
        var (x, y) = CalculateParticipantPos(side);
        var participantBitmap = Images.LoadImage(FetchParticipantBitmapUrl(participant));
        g.DrawImage(participantBitmap, _posX + x, _posY + y);
        if (participant != null && participant.Equipment.IsBroken)
        {
            var broken = Images.LoadImage(Broken);
            g.DrawImage(broken, _posX, _posY);
        }
    }

    // Calculate the position for the drivers
    private static (int x, int y) CalculateParticipantPos(Side side)
    {
        return _currentDirection switch
        {
            Direction.N => side switch
            {
                Side.Left => (20, 20),
                Side.Right => (50, 20),
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            },
            Direction.E => side switch
            {
                Side.Left => (20, 20),
                Side.Right => (20, 50),
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            },
            Direction.S => side switch
            {
                Side.Left => (50, 20),
                Side.Right => (20, 20),
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            },
            Direction.W => side switch
            {
                Side.Left => (20, 50),
                Side.Right => (20, 20),
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static string FetchParticipantBitmapUrl(IParticipant? participant)
    {
        if (participant != null)
            return _currentDirection switch
            {
                Direction.N => participant.TeamColors switch
                {
                    TeamColors.Blue => DriverBlueUp,
                    TeamColors.Green => DriverGreenUp,
                    TeamColors.Grey => DriverGreyUp,
                    TeamColors.Red => DriverRedUp,
                    TeamColors.Yellow => DriverYellowUp,
                    _ => throw new ArgumentOutOfRangeException()
                },
                Direction.E => participant.TeamColors switch
                {
                    TeamColors.Blue => DriverBlueRight,
                    TeamColors.Green => DriverGreenRight,
                    TeamColors.Grey => DriverGreyRight,
                    TeamColors.Red => DriverRedRight,
                    TeamColors.Yellow => DriverYellowRight,
                    _ => throw new ArgumentOutOfRangeException()
                },
                Direction.S => participant.TeamColors switch
                {
                    TeamColors.Blue => DriverBlueDown,
                    TeamColors.Green => DriverGreenDown,
                    TeamColors.Grey => DriverGreyDown,
                    TeamColors.Red => DriverRedDown,
                    TeamColors.Yellow => DriverYellowDown,
                    _ => throw new ArgumentOutOfRangeException()
                },
                Direction.W => participant.TeamColors switch
                {
                    TeamColors.Blue => DriverBlueLeft,
                    TeamColors.Green => DriverGreenLeft,
                    TeamColors.Grey => DriverGreyLeft,
                    TeamColors.Red => DriverRedLeft,
                    TeamColors.Yellow => DriverYellowLeft,
                    _ => throw new ArgumentOutOfRangeException()
                },
                _ => throw new ArgumentOutOfRangeException()
            };
        // Never reached all arms are handled
        return null!;
    }

    private static string GetSectionVisual(SectionTypes sectionType, Direction direction)
    {
        return sectionType switch
        {
            SectionTypes.StartGrid => StartGrid,
            SectionTypes.Finish => direction switch
            {
                Direction.N => FinishNorth,
                Direction.E => FinishEast,
                Direction.S => FinishSouth,
                Direction.W => FinishWest,
                _ => throw new ArgumentOutOfRangeException()
            },
            SectionTypes.Straight => direction switch
            {
                Direction.N => StraightVertical,
                Direction.E => StraightHorizontal,
                Direction.S => StraightVertical,
                Direction.W => StraightHorizontal,
                _ => throw new ArgumentOutOfRangeException()
            },
            SectionTypes.RightCorner => direction switch
            {
                Direction.N => CornerLeftTop,
                Direction.E => CornerRightTop,
                Direction.S => CornerRightBottom,
                Direction.W => CornerLeftBottom,
                _ => throw new ArgumentOutOfRangeException()
            },
            SectionTypes.LeftCorner => direction switch
            {
                Direction.N => CornerRightTop,
                Direction.E => CornerRightBottom,
                Direction.S => CornerLeftBottom,
                Direction.W => CornerLeftTop,
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    // Update the direction if corner is printed
    private static void UpdateDirection(SectionTypes type)
    {
        if (type == SectionTypes.RightCorner)
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
        else if (type == SectionTypes.LeftCorner)
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

    private static void UpdateCursor()
    {
        var size = 100;

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

    #region graphics

    // Track pieces
    private const string StartGrid = @".\assets\sections\track_section_startgrid.png";

    private const string FinishNorth = @".\assets\sections\track_section_finish_vertical.png";
    private const string FinishEast = @".\assets\sections\track_section_finish_horizontal.png";
    private const string FinishSouth = @".\assets\sections\track_section_finish_vertical.png";
    private const string FinishWest = @".\assets\sections\track_section_finish_horizontal.png";

    private const string StraightHorizontal = @".\assets\sections\track_section_straight_horizontal.png";
    private const string StraightVertical = @".\assets\sections\track_section_straight_vertical.png";

    private const string CornerLeftTop = @".\assets\sections\track_section_corner_topleft.png";
    private const string CornerLeftBottom = @".\assets\sections\track_section_corner_bottomleft.png";
    private const string CornerRightTop = @".\assets\sections\track_section_corner_topright.png";
    private const string CornerRightBottom = @".\assets\sections\track_section_corner_bottomright.png";

    // Drivers
    private const string DriverBlueUp = @".\assets\drivers\driver_blue_vertical_top.png";
    private const string DriverBlueDown = @".\assets\drivers\driver_blue_vertical_down.png";
    private const string DriverBlueRight = @".\assets\drivers\driver_blue_horizontal_right.png";
    private const string DriverBlueLeft = @".\assets\drivers\driver_blue_horizontal_left.png";

    private const string DriverGreenUp = @".\assets\drivers\driver_green_vertical_top.png";
    private const string DriverGreenDown = @".\assets\drivers\driver_green_vertical_down.png";
    private const string DriverGreenRight = @".\assets\drivers\driver_green_horizontal_right.png";
    private const string DriverGreenLeft = @".\assets\drivers\driver_green_horizontal_left.png";

    private const string DriverGreyUp = @".\assets\drivers\driver_grey_vertical_top.png";
    private const string DriverGreyDown = @".\assets\drivers\driver_grey_vertical_down.png";
    private const string DriverGreyRight = @".\assets\drivers\driver_grey_horizontal_right.png";
    private const string DriverGreyLeft = @".\assets\drivers\driver_grey_horizontal_left.png";

    private const string DriverRedUp = @".\assets\drivers\driver_red_vertical_top.png";
    private const string DriverRedDown = @".\assets\drivers\driver_red_vertical_down.png";
    private const string DriverRedRight = @".\assets\drivers\driver_red_horizontal_right.png";
    private const string DriverRedLeft = @".\assets\drivers\driver_red_horizontal_left.png";

    private const string DriverYellowUp = @".\assets\drivers\driver_yellow_vertical_top.png";
    private const string DriverYellowDown = @".\assets\drivers\driver_yellow_vertical_down.png";
    private const string DriverYellowRight = @".\assets\drivers\driver_yellow_horizontal_right.png";

    private const string DriverYellowLeft = @".\assets\drivers\driver_yellow_horizontal_left.png";

    // Broken
    private const string Broken = @".\assets\Attributes\BrokenFire.png";

    #endregion
}

// Define the Track directions
public enum Direction
{
    N,
    E,
    S,
    W
}