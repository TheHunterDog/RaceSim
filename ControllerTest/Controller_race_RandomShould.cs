using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Model;

namespace ControllerTest
{
    [TestFixture]
    internal class Controller_race_RandomShould
    {
            private Race _race;
            [SetUp]
            public void SetUp()
            {
                _race = new Race(new Model.Track("Aap", new List<Model.SectionTypes>().ToArray()), new List<IParticipant?>() { new Model.Driver("Mark",2,new Model.Car(3,3,3,false),Model.TeamColors.Blue)});
            }
            [Test]
            public void CheckWin_False()
            {
            //Assert.IsFalse(_race.checkWin());
            }
            [Test]
            public void CheckWin_True()
            {
            //_race.winLimit = -1;
            //Assert.True(_race.checkWin());
            }
            public void Should_Not_Be_Broken()
            {
            Assert.IsFalse(_race.Participants[0].equipment.isBroken);
            }
        //public void Should_be_Broken()
        //{
        //    _race.setRandomBroken();
        //    Assert.IsTrue(_race.Participants[0].equipment.isBroken);
        //}
        }
}
