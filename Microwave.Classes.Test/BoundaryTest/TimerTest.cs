using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Classes.Test.BoundaryTest;

// Only tests of Timer that do not depend on real time passing.
// Time-dependent behaviour is tested in Microwave.Classes.Test.Integration.
[TestFixture]
public class TimerTest
{
    private Timer _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new Timer();
    }

    [TearDown]
    public void TearDown()
    {
        _uut.Stop();
    }

    [TestCase(1)]
    [TestCase(30)]
    [TestCase(3600)]
    public void AddTime_Started_TimeRemainingIncreased(int seconds)
    {
        _uut.Start(60);

        _uut.AddTime(seconds);

        Assert.That(_uut.TimeRemaining, Is.EqualTo(60 + seconds));
    }

    [Test]
    public void AddTime_CalledTwice_TimeAddedBothTimes()
    {
        _uut.Start(60);

        _uut.AddTime(30);
        _uut.AddTime(30);

        Assert.That(_uut.TimeRemaining, Is.EqualTo(120));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-60)]
    public void AddTime_NotPositive_ThrowsException(int seconds)
    {
        _uut.Start(60);

        Assert.That(() => _uut.AddTime(seconds), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void AddTime_NotPositive_TimeRemainingUnchanged()
    {
        _uut.Start(60);

        try { _uut.AddTime(-1); } catch (ArgumentOutOfRangeException) { }

        Assert.That(_uut.TimeRemaining, Is.EqualTo(60));
    }
}
