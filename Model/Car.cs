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

    #endregion

    #region props

    public int Quality
    {
        get => _quality;
        set
        {
            if (value > MinQuality) _quality = value;
        }
    }

    public int Performance
    {
        get => _performance;
        set
        {
            if (value > MinPerformance) _performance = value;
        }
    }

    public int Speed
    {
        get => _speed;
        set
        {
            if (value > MinSpeed) _speed = value;
        }
    }

    public bool IsBroken { get; set; }

    #endregion
}