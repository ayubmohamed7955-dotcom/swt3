using Microwave.Classes.Boundary.Interfaces;
using Microwave.Classes.Controllers;
using Microwave.Classes.Controllers.Interfaces;
using NSubstitute;

namespace Microwave.Classes.Test.ControllerTest;

[TestFixture]
public class MicrowaveControllerMaxPowerTest
{
    private IButton _powerButton;
    private IButton _timeButton;
    private IButton _startCancelButton;
    private IDoor _door;
    private IDisplay _display;
    private ILight _light;
    private ICookController _cooker;
    private IBuzzer _buzzer;
    
    [SetUp]
    public void Setup()
    {
        _powerButton = Substitute.For<IButton>();
        _timeButton = Substitute.For<IButton>();
        _startCancelButton = Substitute.For<IButton>();
        _door = Substitute.For<IDoor>();
        _light = Substitute.For<ILight>();
        _display = Substitute.For<IDisplay>();
        _cooker = Substitute.For<ICookController>();
        _buzzer = Substitute.For<IBuzzer>();
    }

    private MicrowaveController CreateUut(int maxPower)
    {
        return new MicrowaveController(
            _powerButton, _timeButton, _startCancelButton,
            _door, _display, _light, _cooker, _buzzer,
            maxPower);
    }

    private void PressPower(int times)
    {
        for (var i = 1; i <= times; i++)
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        }
    }

    // BVA: nedre grænse, typisk værdi og øvre grænse
    [TestCase(400)]
    [TestCase(1000)]
    [TestCase(1200)]
    public void Ready_PressPowerToMax_DisplayShowsMax(int maxPower)
    {
        CreateUut(maxPower);

        PressPower(maxPower / 50);

        _display.Received(1).ShowPower(Arg.Is(maxPower));
    }

    [TestCase(400)]
    [TestCase(1000)]
    [TestCase(1200)]
    public void Ready_PressPowerOnceMoreThanMax_PowerIs50Again(int maxPower)
    {
        CreateUut(maxPower);

        PressPower(maxPower / 50 + 1);

        _display.Received(2).ShowPower(Arg.Is(50));
    }

    [TestCase(400)]
    [TestCase(1000)]
    [TestCase(1200)]
    public void Ready_PressPowerToMax_NeverShowsMoreThanMax(int maxPower)
    {
        CreateUut(maxPower);

        PressPower(maxPower / 50 + 1);

        _display.DidNotReceive().ShowPower(Arg.Is<int>(p => p > maxPower));
    }

    [TestCase(1000)]
    [TestCase(1200)]
    public void Ready_MaxPowerAndTime_CookerIsCalledWithMax(int maxPower)
    {
        CreateUut(maxPower);

        PressPower(maxPower / 50);
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _cooker.Received(1).StartCooking(maxPower, 60);
    }

    // Max går ikke op i 50: power må aldrig overstige max
    [Test]
    public void Ready_MaxPowerNotMultipleOf50_WrapsBeforeExceedingMax()
    {
        CreateUut(1025);

        PressPower(21); // 20 tryk = 1000, tryk nr. 21 ville give 1050 > 1025

        _display.Received(1).ShowPower(Arg.Is(1000));
        _display.DidNotReceive().ShowPower(Arg.Is(1050));
        _display.Received(2).ShowPower(Arg.Is(50));
    }
}