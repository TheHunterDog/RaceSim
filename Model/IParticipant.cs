using System.ComponentModel;

namespace Model;

public interface IParticipant:INotifyPropertyChanged
{
    string Name { get; set; }
    int Points { get; set; }
    IEquipment Equipment { get; set; }
    TeamColors TeamColors { get; set; }
}