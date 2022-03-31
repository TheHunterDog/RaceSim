using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }
        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Assert.IsNull(_competition.GetNextTrack());
        }
        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track t = new Track("Rotonde", new SectionTypes[]
            {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Finish
            });
            _competition.Tracks.Enqueue(t);
            Assert.AreEqual(t, _competition.GetNextTrack());
        }
        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track t = new Track("Rotonde", new SectionTypes[]
           {
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Finish
           });
            _competition.Tracks.Enqueue(t);
            var result = _competition.GetNextTrack();
            result = _competition.GetNextTrack();

            Assert.IsNull(result);

        }
        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track t = new Track("Rotonde", new SectionTypes[]
{
                SectionTypes.StartGrid,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Finish
});
            _competition.Tracks.Enqueue(t);
            Track a = new Track("Sprint", new SectionTypes[]
            {
                SectionTypes.StartGrid,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Finish
            });
            _competition.Tracks.Enqueue(a);

            var b = _competition.GetNextTrack();
            Assert.AreEqual(t.Name,b.Name);
            Assert.AreEqual(t.Sections, b.Sections);
            b = _competition.GetNextTrack();
            Assert.AreEqual(a.Name, b.Name);
            Assert.AreEqual(a.Sections, b.Sections);
        }
    }
}
