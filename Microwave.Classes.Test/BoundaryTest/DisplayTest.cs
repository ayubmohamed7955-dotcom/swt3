using Microwave.Classes.Boundary;
using Microwave.Classes.Boundary.Interfaces;
using NSubstitute;

namespace Microwave.Classes.Test.BoundaryTest;

[TestFixture]
public class DisplayTest
{
    private Display _uut;
    private IOutput _output;

    [SetUp]
    public void Setup()
    {
        _output = Substitute.For<IOutput>();
        _uut = new Display(_output);
    }

    [Test]
    public void ShowTime_ZeroMinuteZeroSeconds_CorrectOutput()
    {
        _uut.ShowTime(0,0);
        _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:00")));
    }

    [Test]
    public void ShowTime_ZeroMinuteSomeSecond_CorrectOutput()
    {
        _uut.ShowTime(0, 5);
        _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("00:05")));
    }

    [Test]
    public void ShowTime_SomeMinuteZeroSecond_CorrectOutput()
    {
        _uut.ShowTime(5, 0);
        _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("05:00")));
    }

    [Test]
    public void ShowTime_SomeMinuteSomeSecond_CorrectOutput()
    {
        _uut.ShowTime(10, 15);
        _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("10:15")));
    }

    [Test]
    public void ShowPower_Zero_CorrectOutput()
    {
        _uut.ShowPower(0);
        _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("0 W")));
    }

    [Test]
    public void ShowPower_NotZero_CorrectOutput()
    {
        _uut.ShowPower(150);
        _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("150 W")));
    }

    [Test]
    public void Clear_CorrectOutput()
    {
        _uut.Clear();
        _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("cleared")));
    }

}