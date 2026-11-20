using Microwave.Classes.Boundary.Interfaces;
using Microwave.Classes.Controllers;
using Microwave.Classes.Controllers.Interfaces;
using NSubstitute;

namespace Microwave.Classes.Test.ControllerTest;

[TestFixture]
public class CookControllerTest
{
    private CookController _uut;

    private IMicrowaveController _microwaveController;
    private ITimeProvider _timer;
    private IDisplay _display;
    private IPowerTube _powerTube;

    [SetUp]
    public void Setup()
    {
        _microwaveController = Substitute.For<IMicrowaveController>();
        _timer = Substitute.For<ITimeProvider>();
        _display = Substitute.For<IDisplay>();
        _powerTube = Substitute.For<IPowerTube>();

        _uut = new CookController(_timer, _display, _powerTube, _microwaveController);
    }

    [Test]
    public void StartCooking_ValidParameters_TimerStarted()
    {
        _uut.StartCooking(50, 60);

        _timer.Received().Start(60);
    }

    [Test]
    public void StartCooking_ValidParameters_PowerTubeStarted()
    {
        _uut.StartCooking(50, 60);

        _powerTube.Received().TurnOn(50);
    }

    [Test]
    public void Cooking_TimerTick_DisplayCalled()
    {
        _uut.StartCooking(50, 60);

        _timer.TimeRemaining.Returns(115);
        _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

        _display.Received().ShowTime(1, 55);
    }

    [Test]
    public void Cooking_TimerExpired_PowerTubeOff()
    {
        _uut.StartCooking(50, 60);

        _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

        _powerTube.Received().TurnOff();
    }

    [Test]
    public void Cooking_TimerExpired_MicrowaveControllerCalled()
    {
        _uut.StartCooking(50, 60);

        _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

        _microwaveController.Received().CookingIsDone();
    }

    [Test]
    public void Cooking_Stop_PowerTubeOff()
    {
        _uut.StartCooking(50, 60);
        _uut.Stop();

        _powerTube.Received().TurnOff();
    }

    [Test]
    public void AddTime_Cooking_TimerAddTimeCalled()
    {
        _uut.StartCooking(50, 60);

        _uut.AddTime(30);

        _timer.Received(1).AddTime(30);
    }

    [TestCase(30, 0, 30)]
    [TestCase(59, 0, 59)]
    [TestCase(60, 1, 0)]
    [TestCase(90, 1, 30)]
    public void AddTime_Cooking_DisplayShowsNewRemainingTime(int remaining, int min, int sec)
    {
        _uut.StartCooking(50, 60);
        _timer.TimeRemaining.Returns(remaining);

        _uut.AddTime(30);

        _display.Received(1).ShowTime(min, sec);
    }

    [Test]
    public void AddTime_NotStarted_TimerNotCalled()
    {
        _uut.AddTime(30);

        _timer.DidNotReceive().AddTime(Arg.Any<int>());
        _display.DidNotReceive().ShowTime(Arg.Any<int>(), Arg.Any<int>());
    }

    [Test]
    public void AddTime_AfterStop_TimerNotCalled()
    {
        _uut.StartCooking(50, 60);
        _uut.Stop();

        _uut.AddTime(30);

        _timer.DidNotReceive().AddTime(Arg.Any<int>());
    }

    [Test]
    public void AddTime_AfterTimerExpired_TimerNotCalled()
    {
        _uut.StartCooking(50, 60);
        _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

        _uut.AddTime(30);

        _timer.DidNotReceive().AddTime(Arg.Any<int>());
    }
}
