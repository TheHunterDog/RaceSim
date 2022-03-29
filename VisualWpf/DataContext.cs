using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;

namespace WPF
{
    public class DataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public Func<String> Track = () =>
        {
            return Data.CurrentRace.Track.Name;
        };
        public String TrackName { get; set; }
        public void OnDriverChanged(object sender, DriversChangedEventArgs e)
        {
            TrackName = e.track.Name;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
