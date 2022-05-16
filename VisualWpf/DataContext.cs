using System.ComponentModel;
using Model;

namespace WPF;

public class DataContext : INotifyPropertyChanged
{
    public string? Trackname { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnDriverChanged(object sender, DriversChangedEventArgs e)
    {
        if (e.Track?.Name != null) Trackname = e.Track?.Name;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
    }

    private void OnPropertyChanged(string s)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
    }
}