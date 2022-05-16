namespace Model;

public class Competition
{
    #region constructor

    public Competition()
    {
        Participants = new List<IParticipant?>();
        Tracks = new Queue<Track>();
    }

    #endregion

    #region methods

    /**
     * public Track? GetNextTrack()
     * De volgende track wordt opgehaald als er geen volgede track is wordt er null terug gegeven
     */
    public Track? GetNextTrack()
    {
        return Tracks.Count > 0 ? Tracks.Dequeue() : null;
    }

    #endregion

    #region props

    public List<IParticipant?>? Participants { get; set; }
    public Queue<Track> Tracks { get; set; }

    #endregion
}