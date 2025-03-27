using Microwave.Classes.Boundary;

namespace Microwave.Classes.Test.BoundaryTest;

[TestFixture]
public class DoorTest
{
    private Door _uut;

    [SetUp]
    public void Setup()
    {
        _uut = new Door();
    }

    [Test]
    public void Open_NoSubscribers_NoThrow()
    {
        // We don't need an assert, as an exception would fail the test case
        _uut.Open();
    }

    [Test]
    public void Open_1subscriber_IsNotified()
    {
        bool notified = false;

        _uut.Opened += (sender, args) => notified = true;
        _uut.Open();
        Assert.That(notified, Is.EqualTo(true));
    }

    [Test]
    public void Close_NoSubscribers_NoThrow()
    {
        // We don't need an assert, as an exception would fail the test case
        _uut.Close();
    }

    [Test]
    public void Close_1subscriber_IsNotified()
    {
        bool notified = false;

        _uut.Closed += (sender, args) => notified = true;
        _uut.Close();
        Assert.That(notified, Is.EqualTo(true));
    }

}