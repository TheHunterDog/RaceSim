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
        public String Windesheim { get; set; }
        public DateTime CurrentTime { get; set; }
        public String TrackName { get; set; }
        public void OnDriverChanged(object sender, DriversChangedEventArgs e)
        {
            TrackName = e.track.Name;
            changeText();
            CurrentTime = DateTime.Now;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
        public void changeText()
        {
            if(Windesheim == "Windesheim")
            {
                Windesheim = "Hello world";
            }
            else
            {
                Windesheim = "Windesheim";
            }
    }   
    }
}

