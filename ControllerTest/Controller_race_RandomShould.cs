using System.Collections.Generic;
using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest;

[TestFixture]
internal class ControllerRaceRandomShould
{
    public ControllerRaceRandomShould(Race race)
    {
        SetUp();
    }

    [SetUp]
    public void SetUp()
    {
        _race = new Race(new Track("Aap", new List<SectionTypes>().ToArray()),
            new List<IParticipant?> {new Driver("Mark", 2, new Car(3, 3, 3, false), TeamColors.Blue)});
    }

    private Race _race = null!;

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
        Assert.IsFalse(_race.Participants != null && _race.Participants[0]!.Equipment.IsBroken);
    }
    //public void Should_be_Broken()
    //{
    //    _race.setRandomBroken();
    //    Assert.IsTrue(_race.Participants[0].equipment.isBroken);
    //}
}