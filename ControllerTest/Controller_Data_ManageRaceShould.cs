using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using Model;
using Controller;

namespace ControllerTest
{
    [TestFixture]
    internal class Controller_Data_ManageRaceShould
    {
        private Competition _competition;
        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }
        [Test]
        public void Init_NotNull()
        {
            Data.Initialize(_competition);
            Data.StartNextRace(_competition);
            Assert.NotNull(Data.Competition);
            Assert.NotNull(Data.CurrentRace);
            Assert.NotNull(Data.Competition.Participants);
        }
        [Test]
        public void NextRace_Should_Exist()
        {
            Data.StartNextRace(_competition);
            Assert.NotNull(Data.CurrentRace);
        }
        [Test]
        public void NextRace_Should_NotExist()
        {
            Data.StartNextRace(_competition);
            Assert.IsNull(Data.CurrentRace);
        }
    }
}
