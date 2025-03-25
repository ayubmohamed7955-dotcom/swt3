using Microwave.Classes.Boundary;

namespace Microwave.Classes.Test.BoundaryTest;

[TestFixture]
public class ButtonTest
{
    private Button uut;

    [SetUp]
    public void Setup()
    {
        uut = new Button();
    }

    [Test]
    public void Press_NoSubscribers_NoThrow()
    {
        // We don't need an assert, as an exception would fail the test case
        uut.Press();
    }

    [Test]
    public void Press_1subscriber_IsNotified()
    {
        var notified = false;

        uut.Pressed += (_, _) => notified = true;
        uut.Press();
        Assert.That(notified, Is.EqualTo(true));
    }

}