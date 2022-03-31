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
        #region props
        public static Model.Competition? Competition { get; set; }
        public static Controller.Race? CurrentRace { get; set; }

        public static event EventHandler<NextRaceEventArgs> NextRaceEvent;
        #endregion

        #region Methods
        public static void Initialize(Competition c)
        {
            Competition = new Competition();
            AddParticipants();
            AddTracks();
        }

        public static void StartNextRace()
        {
            CurrentRace?.Clean();
            Track nextTrack = Competition.GetNextTrack();
            if (nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.Participants);
                CurrentRace.End += OnRaceFinished;
                NextRaceEvent?.Invoke(null, new NextRaceEventArgs()
                {
                    Race = CurrentRace
                });
                CurrentRace.Start();


            }
        }
        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver("Mark", 33, new Car(100, 60, 40, false), TeamColors.Blue));
            Competition.Participants.Add(new Driver("Gert", 55, new Car(100, 60, 40, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("Gerwin", 1, new Car(100, 60, 60, false), TeamColors.Green));
            Competition.Participants.Add(new Driver("Rick", 44, new Car(100, 60, 120, false), TeamColors.Yellow));
            Competition.Participants.Add(new Driver("Gerwin", 1, new Car(100, 100, 100, false), TeamColors.Green));
            Competition.Participants.Add(new Driver("Rick", 44, new Car(100, 100, 100, false), TeamColors.Yellow));

            //Competition.Participants.Add(new Driver("DR.ROBOT", 100, new Car(20, 20, 20, false), TeamColors.Grey));
        }
        public static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("Circuit Assen", new SectionTypes[]
{
                SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight
}));
            Competition.Tracks.Enqueue(new Track("Circuit Assen", new SectionTypes[]
{
                SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight
}));

            Competition.Tracks.Enqueue(new Track("Circuit Zandvoort", new SectionTypes[]
            {
                SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish
            }));
        }
    
        public static void OnRaceFinished(object sender,EventArgs args)
        {
            StartNextRace();
        }
        #endregion
    }
}
