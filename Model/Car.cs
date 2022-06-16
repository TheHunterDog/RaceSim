using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model;

public class Car : IEquipment
{
    #region constructor

    public Car(int quality, int performance, int speed, bool isBroken)
    {
        Quality = quality;
        Performance = performance;
        Speed = speed;
        IsBroken = isBroken;
    }

    #endregion

    #region private_varables

    private const int MinQuality = 20;
    private const int MinPerformance = 20;
    private const int MinSpeed = 20;

    private int _quality;
    private int _performance;
    private int _speed;
    private bool _isBroken;

    #endregion

    #region props

    public int Quality
    {
        get => _quality;
        set
        {
            if (value > MinQuality) _quality = value;
            OnPropertyChanged();

        }
    }

    public int Performance
    {
        get => _performance;
        set
        {
            if (value > MinPerformance) _performance = value;
            OnPropertyChanged();

        }
    }

    public int Speed
    {
        get => _speed;
        set
        {
            if (value > MinSpeed) _speed = value;
            OnPropertyChanged();
        }
    }

    public bool IsBroken
    {
        get => _isBroken;
        set
        {
            _isBroken = value;
            OnPropertyChanged();

        }
    }

    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}