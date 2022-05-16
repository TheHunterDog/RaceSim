namespace Model;

public class Driver : IParticipant
{
    #region constructor

    public Driver(string name, int points, IEquipment equipment, TeamColors teamColors)
    {
        Name = name;
        Points = points;
        Equipment = equipment;
        TeamColors = teamColors;
    }

    #endregion

    #region props

    public string Name { get; set; }
    public int Points { get; set; }
    public IEquipment Equipment { get; set; }
    public TeamColors TeamColors { get; set; }

    #endregion
}