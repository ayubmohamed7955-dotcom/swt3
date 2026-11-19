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

    #region Default max power (700 W)

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
    public void Ctor_NoMaxPowerGiven_MaxPowerIs700()
    {
        Assert.That(_uut.MaxPower, Is.EqualTo(700));
    }

    #endregion

    #region Configurable max power (400-1200 W)
    [TestCase(400)]
    [TestCase(401)]
    [TestCase(800)]
    [TestCase(1199)]
    [TestCase(1200)]
    public void Ctor_ValidMaxPower_MaxPowerIsSet(int maxPower)
    {
        var uut = new PowerTube(_output, maxPower);
        Assert.That(uut.MaxPower, Is.EqualTo(maxPower));
    }
    
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(399)]
    [TestCase(1201)]
    [TestCase(2000)]
    public void Ctor_InvalidMaxPower_ThrowsException(int maxPower)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PowerTube(_output, maxPower));
    }
    
    [TestCase(400, 1)]
    [TestCase(400, 400)]
    [TestCase(1000, 999)]
    [TestCase(1000, 1000)]
    [TestCase(1200, 1200)]
    public void TurnOn_ConfiguredMax_PowerWithinRange_CorrectOutput(int maxPower, int power)
    {
        var uut = new PowerTube(_output, maxPower);
        uut.TurnOn(power);
        _output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
    }

    [TestCase(400, 0)]
    [TestCase(400, 401)]
    [TestCase(1000, 1001)]
    [TestCase(1200, 1201)]
    public void TurnOn_ConfiguredMax_PowerOutOfRange_ThrowsException(int maxPower, int power)
    {
        var uut = new PowerTube(_output, maxPower);
        Assert.Throws<ArgumentOutOfRangeException>(() => uut.TurnOn(power));
    }

    #endregion

    #region TurnOn/TurnOff state

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
    
    #endregion
}