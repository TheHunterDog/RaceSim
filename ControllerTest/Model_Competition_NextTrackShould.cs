using Model;
using NUnit.Framework;

namespace ControllerTest;

[TestFixture]
public class ModelCompetitionNextTrackShould
{
    public ModelCompetitionNextTrackShould(Competition competition)
    {
        _competition = competition;
        SetUp();
    }

    [SetUp]
    public void SetUp()
    {
        _competition = new Competition();
    }

    private Competition _competition;

    [Test]
    public void NextTrack_EmptyQueue_ReturnNull()
    {
        Assert.IsNull(_competition.GetNextTrack());
    }

    [Test]
    public void NextTrack_OneInQueue_ReturnTrack()
    {
        var t = new Track("Rotonde", new[]
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
        var t = new Track("Rotonde", new[]
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
        var t = new Track("Rotonde", new[]
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
        var a = new Track("Sprint", new[]
        {
            SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Finish
        });
        _competition.Tracks.Enqueue(a);

        var b = _competition.GetNextTrack();
        Assert.AreEqual(t.Name, b?.Name);
        Assert.AreEqual(t.Sections, b?.Sections);
        b = _competition.GetNextTrack();
        Assert.AreEqual(a.Name, b?.Name);
        Assert.AreEqual(a.Sections, b?.Sections);
    }
}