namespace Model;

public class SectionData
{
    public IParticipant? Left { get; set; }
    public IParticipant? Right { get; set; }
    public int DistanceLeft { get; set; }
    public int DistacneRight { get; set; }

    public bool AddParticipantToSection(IParticipant? p)
    {
        if (Right == null)
        {
            Right = p;
            return true;
        }

        if (Left == null)
        {
            Left = p;
            return true;
        }

        return false;
    }
}