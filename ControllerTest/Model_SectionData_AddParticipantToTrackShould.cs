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
    public class Model_SectionData_AddParticipantToTrackShould
    {
        private SectionData _SectionData;
        private Driver d;
        [SetUp]
        public void SetUp()
        {
            _SectionData = new SectionData();
            d = new Driver("Mark", 0, new Car(0, 0, 0, false), TeamColors.Green);
        }
        [Test]
        public void AddParticpantToTrack_ReturnTrue()
        {
            Assert.IsTrue(_SectionData.addParicpantTpSection(d));
        }
        [Test]
        public void AddParticipantToTrack_ReturnFalse()
        {
            for (int i = 0; i <= 3; i++)
            {
                _SectionData.addParicpantTpSection(d);
            }
            Assert.IsFalse(_SectionData.addParicpantTpSection(d));
        }        
    }
}
