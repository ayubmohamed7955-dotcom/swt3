using Microwave.Classes.Boundary;
using Microwave.Classes.Boundary.Interfaces;
using NSubstitute;

namespace Microwave.Classes.Test.BoundaryTest;

[TestFixture]
public class BuzzerTest
{
    private Buzzer _uut;
    private IOutput _output;

    [SetUp]
    public void Setup()
    {
        _output = Substitute.For<IOutput>();

        _uut = new Buzzer(_output);
    }

    // BVA: 1 is the lowest valid number of bursts, 3 is the nominal value used when cooking is done
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void Buzz_ValidBursts_OutputsOneLinePerBurst(int bursts)
    {
        _uut.Buzz(bursts);
        _output.Received(bursts).OutputLine(Arg.Is<string>(str => str.Contains("Buzzer")));
    }

    [Test]
    public void Buzz_OneBurst_CorrectOutputFormat()
    {
        _uut.Buzz(1);
        _output.Received(1).OutputLine("Buzzer: BZZZ");
    }

    // BVA: 0 is the first invalid value below the boundary, -1 represents negative input
    [TestCase(0)]
    [TestCase(-1)]
    public void Buzz_InvalidBursts_ThrowsArgumentOutOfRange(int bursts)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _uut.Buzz(bursts));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void Buzz_InvalidBursts_NoOutput(int bursts)
    {
        try { _uut.Buzz(bursts); } catch (ArgumentOutOfRangeException) { }
        _output.DidNotReceiveWithAnyArgs().OutputLine(default!);
    }
}
