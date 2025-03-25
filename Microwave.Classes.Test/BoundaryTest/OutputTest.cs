using Microwave.Classes.Boundary;

namespace Microwave.Classes.Test.BoundaryTest;

[TestFixture]
class OutputTest
{
    private StringWriter str;
    private Output uut;

    [SetUp]
    public void Setup()
    {
        str = new StringWriter();
        Console.SetOut(str);
        uut = new Output();
    }

    [Test]
    public void OutputLineCorrect()
    {
        uut.OutputLine("Test");

        Assert.That(str.ToString().Contains("Test"));
    }
}
