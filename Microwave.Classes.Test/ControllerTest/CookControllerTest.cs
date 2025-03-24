using Microwave.Classes.Boundary.Interfaces;
using Microwave.Classes.Controllers;
using Microwave.Classes.Controllers.Interfaces;
using Microwave.Classes.Domain.Interfaces;
using NSubstitute;

namespace Microwave.Classes.Test.ControllerTest;

[TestFixture]
public class CookControllerTest
{
    private CookController uut;

    private IMicrowaveController ui;
    private ITimeProvider timer;
    private IDisplay display;
    private IPowerTube powerTube;

    [SetUp]
    public void Setup()
    {
        ui = Substitute.For<IMicrowaveController>();
        timer = Substitute.For<ITimeProvider>();
        display = Substitute.For<IDisplay>();
        powerTube = Substitute.For<IPowerTube>();

        uut = new CookController(timer, display, powerTube, ui);
    }

    [Test]
    public void StartCooking_ValidParameters_TimerStarted()
    {
        uut.StartCooking(50, 60);

        timer.Received().Start(60);
    }

    [Test]
    public void StartCooking_ValidParameters_PowerTubeStarted()
    {
        uut.StartCooking(50, 60);

        powerTube.Received().TurnOn(50);
    }

    [Test]
    public void Cooking_TimerTick_DisplayCalled()
    {
        uut.StartCooking(50, 60);

        timer.TimeRemaining.Returns(115);
        timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

        display.Received().ShowTime(1, 55);
    }

    [Test]
    public void Cooking_TimerExpired_PowerTubeOff()
    {
        uut.StartCooking(50, 60);

        timer.Expired += Raise.EventWith(this, EventArgs.Empty);

        powerTube.Received().TurnOff();
    }

    [Test]
    public void Cooking_TimerExpired_UICalled()
    {
        uut.StartCooking(50, 60);

        timer.Expired += Raise.EventWith(this, EventArgs.Empty);

        ui.Received().CookingIsDone();
    }

    [Test]
    public void Cooking_Stop_PowerTubeOff()
    {
        uut.StartCooking(50, 60);
        uut.Stop();

        powerTube.Received().TurnOff();
    }

}