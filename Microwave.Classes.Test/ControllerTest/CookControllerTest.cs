using Microwave.Classes.Boundary.Interfaces;
using Microwave.Classes.Controllers;
using Microwave.Classes.Controllers.Interfaces;
using NSubstitute;

namespace Microwave.Classes.Test.ControllerTest;

[TestFixture]
public class CookControllerTest
{
    private CookController _uut;

    private IMicrowaveController _ui;
    private ITimeProvider _timer;
    private IDisplay _display;
    private IPowerTube _powerTube;

    [SetUp]
    public void Setup()
    {
        _ui = Substitute.For<IMicrowaveController>();
        _timer = Substitute.For<ITimeProvider>();
        _display = Substitute.For<IDisplay>();
        _powerTube = Substitute.For<IPowerTube>();

        _uut = new CookController(_timer, _display, _powerTube, _ui);
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
    public void Cooking_TimerExpired_UICalled()
    {
        _uut.StartCooking(50, 60);

        _timer.Expired += Raise.EventWith(this, EventArgs.Empty);

        _ui.Received().CookingIsDone();
    }

    [Test]
    public void Cooking_Stop_PowerTubeOff()
    {
        _uut.StartCooking(50, 60);
        _uut.Stop();

        _powerTube.Received().TurnOff();
    }

}