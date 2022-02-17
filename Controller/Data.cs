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

        public static void Initialize(Competition comp)
        {
            Competition = comp;
            AddParticipants();
            AddTracks();
        }

        public static void NextRace()
        {
            Track nextTrack = Competition.NextTrack();
            if (nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.participants);
            }
        }
        public static void AddParticipants()
        {
            Competition.participants.Add(new Driver("Mark",33,new Car(100,100,100,false),TeamColors.Blue));
            Competition.participants.Add(new Driver("Gert",55,new Car(30,20,10,false),TeamColors.Red));
            Competition.participants.Add(new Driver("Gerwin",1,new Car(1,1,1,false),TeamColors.Green));
            Competition.participants.Add(new Driver("Rick",44,new Car(100,100,100,false),TeamColors.Yellow));
            Competition.participants.Add(new Driver("DR.ROBOT",100,new Car(200,200,200,false),TeamColors.Grey));
        }
        public static void AddTracks()
        {
            Competition.tracks.Enqueue(new Track("Rotonde", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Finish
            }));
            Competition.tracks.Enqueue(new Track("Rondje van de zaak", new SectionTypes[]
{
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Finish
})); ;
            Competition.tracks.Enqueue(new Track("111m drag race", new SectionTypes[]
{
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Finish,
}));
        }
    }
}
