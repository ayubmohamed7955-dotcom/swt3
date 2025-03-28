using Microwave.Classes.Boundary;
using Microwave.Classes.Boundary.Interfaces;
using NSubstitute;

namespace Microwave.Classes.Test.BoundaryTest;

[TestFixture]
public class PowerTubeTest
{
    private PowerTube _uut;
    private IOutput _output;

    [SetUp]
    public void Setup()
    {
        _output = Substitute.For<IOutput>();
        _uut = new PowerTube(_output);
    }

    [TestCase(1)]
    [TestCase(50)]
    [TestCase(100)]
    [TestCase(699)]
    [TestCase(700)]
    public void TurnOn_WasOffCorrectPower_CorrectOutput(int power)
    {
        _uut.TurnOn(power);
        _output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
    }

    [TestCase(-5)]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(701)]
    [TestCase(750)]
    public void TurnOn_WasOffOutOfRangePower_ThrowsException(int power)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _uut.TurnOn(power));
    }

    [Test]
    public void TurnOff_WasOn_CorrectOutput()
    {
        _uut.TurnOn(50);
        _uut.TurnOff();
        _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
    }

    [Test]
    public void TurnOff_WasOff_NoOutput()
    {
        _uut.TurnOff();
        _output.DidNotReceive().OutputLine(Arg.Any<string>());
    }

    [Test]
    public void TurnOn_WasOn_ThrowsException()
    {
        _uut.TurnOn(50);
        Assert.Throws<ApplicationException>(() => _uut.TurnOn(60));
    }
}
