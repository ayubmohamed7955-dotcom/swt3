using Microwave.Classes.Boundary;

namespace Microwave.Classes.Test.BoundaryTest;

[TestFixture]
public class OutputTest
{
    private StringWriter _str;
    private Output _uut;

    [SetUp]
    public void Setup()
    {
        _str = new StringWriter();
        Console.SetOut(_str);
        _uut = new Output();
    }

    [Test]
    public void OutputLineCorrect()
    {
        _uut.OutputLine("Test");

        Assert.That(_str.ToString().Contains("Test"));
    }
}
