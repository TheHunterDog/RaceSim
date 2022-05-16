namespace Model;

public interface IParticipant
{
    string Name { get; set; }
    int Points { get; set; }
    IEquipment Equipment { get; set; }
    TeamColors TeamColors { get; set; }
}