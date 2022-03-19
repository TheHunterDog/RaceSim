using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Model.Competition Competition { get; set; }
        public static Controller.Race CurrentRace { get; set; }

        public static event EventHandler<NextRaceEventArgs> NextRaceEvent;

        public static void Initialize(Competition comp)
        {
            Competition = comp;
            AddParticipants();
            AddTracks();

        }

        public static void NextRace()
        {
            CurrentRace?.Clean();
            Track nextTrack = Competition.NextTrack();
            if (nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.participants);
                CurrentRace.End += OnRaceFinished;
                NextRaceEvent?.Invoke(null, new NextRaceEventArgs()
                {
                    race = CurrentRace
                });
                CurrentRace.Start();


            }
        }
        public static void AddParticipants()
        {
            Competition.participants.Add(new Driver("Mark", 33, new Car(20, 20, 20, false), TeamColors.Blue));
            Competition.participants.Add(new Driver("Gert", 55, new Car(30, 20, 10, false), TeamColors.Red));
            Competition.participants.Add(new Driver("Gerwin", 1, new Car(20, 20, 20, false), TeamColors.Green));
            Competition.participants.Add(new Driver("Rick", 44, new Car(20, 20, 20, false), TeamColors.Yellow));
            Competition.participants.Add(new Driver("Gerwin", 1, new Car(20, 20, 20, false), TeamColors.Green));
            Competition.participants.Add(new Driver("Rick", 44, new Car(20, 20, 20, false), TeamColors.Yellow));

            Competition.participants.Add(new Driver("DR.ROBOT", 100, new Car(20, 20, 20, false), TeamColors.Grey));
        }
        public static void AddTracks()
        {
            Competition.tracks.Enqueue(new Track("Circuit Assen", new SectionTypes[]
{
                SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight
}));

            Competition.tracks.Enqueue(new Track("Circuit Zandvoort", new SectionTypes[]
            {
                SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish
            }));
        }
    
        public static void OnRaceFinished(object sender,EventArgs args)
        {
            NextRace();
        }
    }
}
