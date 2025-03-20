using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Classes.Test.BoundaryTest;

[TestFixture]
public class TimerTest
{
    private Timer uut;

    [SetUp]
    public void Setup()
    {
        uut = new Timer();
    }

    [Test]
    public void Start_TimerTick_ShortEnough()
    {
        var pause = new ManualResetEvent(false);

        uut.TimerTick += (_, _) => pause.Set();
        uut.Start(2);

        // wait for a tick, but no longer
        Assert.That(pause.WaitOne(1100));
    }

    [Test]
    public void Start_TimerTick_LongEnough()
    {
        var pause = new ManualResetEvent(false);

        uut.TimerTick += (_, _) => pause.Set();
        uut.Start(2);

        // wait shorter than a tick, shouldn't come
        Assert.That(!pause.WaitOne(900));
    }

    [Test]
    public void Start_TimerExpires_ShortEnough()
    {
        var pause = new ManualResetEvent(false);

        uut.Expired += (_, _) => pause.Set();
        uut.Start(2);

        // wait for expiration, but not much longer, should come
        Assert.That(pause.WaitOne(2100));
    }

    [Test]
    public void Start_TimerExpires_LongEnough()
    {
        var pause = new ManualResetEvent(false);

        uut.Expired += (_, _) => pause.Set();
        uut.Start(2);

        // wait shorter than expiration, shouldn't come
        Assert.That(!pause.WaitOne(1900));
    }

    [Test]
    public void Start_TimerTick_CorrectNumber()
    {
        var pause = new ManualResetEvent(false);
        var notifications = 0;

        uut.Expired += (_, _) => pause.Set();
        uut.TimerTick += (_, _) => notifications++;

        uut.Start(2);
        Assert.Multiple(() =>
        {
            // wait longer than expiration
            Assert.That(pause.WaitOne(2100));

            Assert.That(notifications, Is.EqualTo(2));
        });
    }

    [Test]
    public void Stop_NotStarted_NoThrow()
    {
        Assert.That( () => uut.Stop(), Throws.Nothing);
    }

    [Test]
    public void Stop_Started_NoTickTriggered()
    {
        var pause = new ManualResetEvent(false);

        uut.TimerTick += (_, _) => pause.Set();

        uut.Start(2000);
        uut.Stop();

        Assert.That(!pause.WaitOne(1100));
    }

    [Test]
    public void Stop_Started_NoExpiredTriggered()
    {
        var pause = new ManualResetEvent(false);

        uut.Expired += (_, _) => pause.Set();

        uut.Start(2000);
        uut.Stop();

        Assert.That(!pause.WaitOne(2100));
    }

    [Test]
    public void Stop_StartedOneTick_NoExpiredTriggered()
    {
        var pause = new ManualResetEvent(false);

        uut.Expired += (_, _) => pause.Set();
        uut.TimerTick += (_, _) => uut.Stop();

        uut.Start(2000);

        Assert.That(!pause.WaitOne(2100));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public void Tick_Started_TimeRemainingCorrect(int ticks)
    {
        var pause = new ManualResetEvent(false);
        var ticksGone = 0;
        uut.TimerTick += (_, _) =>
        {
            ticksGone++;
            if (ticksGone >= ticks)
                pause.Set();
        };
        uut.Start(5);

        // wait for ticks, only a little longer
        pause.WaitOne(ticks * 1000 + 100);

        Assert.That(uut.TimeRemaining, Is.EqualTo(5-ticks*1));
    }
}