using Microwave.Classes.Boundary;
using Microwave.Classes.Boundary.Interfaces;
using NSubstitute;

namespace Microwave.Classes.Test.BoundaryTest;

[TestFixture]
public class LightTest
{
    private Light _uut;
    private IOutput _output;

    [SetUp]
    public void Setup()
    {
        _output = Substitute.For<IOutput>();

        _uut = new Light(_output);
    }

    [Test]
    public void TurnOn_WasOff_CorrectOutput()
    {
        _uut.TurnOn();
        _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("on")));
    }

    [Test]
    public void TurnOff_WasOn_CorrectOutput()
    {
        _uut.TurnOn();
        _uut.TurnOff();
        _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
    }

    [Test]
    public void TurnOn_WasOn_CorrectOutput()
    {
        _uut.TurnOn();
        _uut.TurnOn();
        _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("on")));
    }

    [Test]
    public void TurnOff_WasOff_CorrectOutput()
    {
        _uut.TurnOff();
        _output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("off")));
    }



}