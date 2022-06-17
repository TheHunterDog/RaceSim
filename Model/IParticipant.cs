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
            participant.Equipment.Performance = r.Next(0,5);
            participant.Equipment.Quality = r.Next(0,6);
            participant.IsFinished = false;

        }
        return p;
    }
}