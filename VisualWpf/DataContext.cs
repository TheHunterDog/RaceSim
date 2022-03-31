using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Microsoft.VisualBasic.CompilerServices;
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
        private String _windesheim = "aap";
        public String Windesheim {
            get => _windesheim;
            set 
            { 
                _windesheim = value;
                OnPropertyChanged(nameof(Windesheim) );
            } 
        }
        public DateTime CurrentTime { get; private set; }
        public String TrackName { get; set; }
        public void OnDriverChanged(object sender, DriversChangedEventArgs e)
        {
            TrackName = e.Track.Name;
            changeText();
            CurrentTime = DateTime.Now;
           // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
        private void OnPropertyChanged(string s)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
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

