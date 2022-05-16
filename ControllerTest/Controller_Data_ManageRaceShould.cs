using Controller;
using Model;
using NUnit.Framework;

namespace ControllerTest;

[TestFixture]
internal class ControllerDataManageRaceShould
{
    [SetUp]
    public void SetUp()
    {
        _competition = new Competition();
    }

    private Competition? _competition;

    [Test]
    public void Init_NotNull()
    {
        Data.Initialize(_competition);
        Data.StartNextRace(_competition);
        Assert.NotNull(Data.Competition);
        Assert.NotNull(Data.CurrentRace);
        Assert.NotNull(Data.Competition?.Participants);
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