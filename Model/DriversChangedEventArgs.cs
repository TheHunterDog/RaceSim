namespace Model;

public class DriversChangedEventArgs : EventArgs
{
    #region Constructor

    public DriversChangedEventArgs(Track? t)
    {
        Track = t;
    }

    #endregion

    #region props

    public Track? Track { get; }

    #endregion
}