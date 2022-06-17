using System.ComponentModel;

namespace Model;

public interface IParticipant:INotifyPropertyChanged
{
    string Name { get; set; }
    int Points { get; set; }
    IEquipment Equipment { get; set; }
    TeamColors TeamColors { get; set; }
    bool IsFinished { get; set; }

    static List<IParticipant> Reset(List<IParticipant> p)
    {
        foreach (var participant in p)
        {
            Random r = new Random();
            participant.Points = r.Next();
            participant.Equipment.Performance = r.Next();
            participant.Equipment.Quality = r.Next();
            participant.Equipment.Speed = r.Next();
            participant.IsFinished = false;

        }
        return p;
    }
}