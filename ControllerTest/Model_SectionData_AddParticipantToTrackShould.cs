using Model;
using NUnit.Framework;

namespace ControllerTest;

[TestFixture]
public class ModelSectionDataAddParticipantToTrackShould
{
    [SetUp]
    public void SetUp()
    {
        _sectionData = new SectionData();
        _d = new Driver("Mark", 0, new Car(0, 0, 0, false), TeamColors.Green);
    }

    private SectionData? _sectionData;
    private Driver? _d;

    [Test]
    public void AddParticpantToTrack_ReturnTrue()
    {
        Assert.IsTrue(_sectionData != null && _sectionData.AddParticipantToSection(_d));
    }

    [Test]
    public void AddParticipantToTrack_ReturnFalse()
    {
        for (var i = 0; i <= 3; i++) _sectionData?.AddParticipantToSection(_d);
        Assert.IsFalse(_sectionData != null && _sectionData.AddParticipantToSection(_d));
    }
}