using System.ComponentModel;
using System.Runtime.CompilerServices;

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

    private IEquipment _equipment;
    private int _points;
    private string _name;
    
    #region props

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public int Points { get => _points;
        set
        {
            _points = value;
            OnPropertyChanged();

        }
    }

    public IEquipment Equipment {
        get
        {
            return _equipment;
        }

        set
        {
            _equipment = value;
              OnPropertyChanged();  
            }
    }

    public TeamColors TeamColors { get; set; }

    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}