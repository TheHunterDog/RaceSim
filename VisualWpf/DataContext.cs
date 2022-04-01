using System;
using System.ComponentModel;
using Controller;
using Model;

namespace WPF
{
    public class DataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public Func<string> Track = () => Data.CurrentRace == null ? "DEFAULT" : Data.CurrentRace.Track.Name;
        private string _windesheim = "aap";
        public string Windesheim {
            get => _windesheim;
            set 
            { 
                _windesheim = value;
                OnPropertyChanged(nameof(Windesheim) );
            } 
        }
        public DateTime CurrentTime { get; private set; }
        public String? TrackName { get; set; }
        public void OnDriverChanged(object sender, DriversChangedEventArgs e)
        {
            TrackName = e.Track.Name;
            ChangeText();
            CurrentTime = DateTime.Now;
           // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
        private void OnPropertyChanged(string s)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
        }

        private void ChangeText()
        {
            Windesheim = Windesheim == "Windesheim" ? "Hello world" : "Windesheim";
        }   
    }
}

