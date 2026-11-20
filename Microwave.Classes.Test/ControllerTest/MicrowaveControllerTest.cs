using Microwave.Classes.Boundary.Interfaces;
using Microwave.Classes.Controllers;
using Microwave.Classes.Controllers.Interfaces;
using NSubstitute;

namespace Microwave.Classes.Test.ControllerTest;

[TestFixture]
public class MicrowaveControllerTest
{
    private MicrowaveController _uut;

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

        _uut = new MicrowaveController(
            _powerButton, _timeButton, _startCancelButton,
            _door,
            _display,
            _light,
            _cooker,
            _buzzer);
    }

    [Test]
    public void Ready_DoorOpen_LightOn()
    {
        // This tests that the uut has subscribed to door opened, and works correctly
        // simulating the event through NSubstitute
        _door.Opened += Raise.EventWith(this, EventArgs.Empty);
        _light.Received().TurnOn();
    }

    [Test]
    public void DoorOpen_DoorClose_LightOff()
    {
        // This tests that the uut has subscribed to door opened and closed, and works correctly
        // simulating the event through NSubstitute
        _door.Opened += Raise.EventWith(this, EventArgs.Empty);
        _door.Closed += Raise.EventWith(this, EventArgs.Empty);
        _light.Received().TurnOff();
    }

    [Test]
    public void Ready_DoorOpenClose_Ready_PowerIs50()
    {
        // This tests that the uut has subscribed to power button, and works correctly
        // simulating the events through NSubstitute
        _door.Opened += Raise.EventWith(this, EventArgs.Empty);
        _door.Closed += Raise.EventWith(this, EventArgs.Empty);

        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        _display.Received(1).ShowPower(Arg.Is(50));
    }

    [Test]
    public void Ready_2PowerButton_PowerIs100()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        _display.Received(1).ShowPower(Arg.Is(100));
    }

    [Test]
    public void Ready_14PowerButton_PowerIs700()
    {
        for (var i = 1; i <= 14; i++)
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        }
        _display.Received(1).ShowPower(Arg.Is(700));
    }

    [Test]
    public void Ready_15PowerButton_PowerIs50Again()
    {
        for (var i = 1; i <= 15; i++)
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        }
        // And then once more
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        _display.Received(2).ShowPower(50);
    }

    [Test]
    public void SetPower_CancelButton_DisplayCleared()
    {
        // Also checks if TimeButton is subscribed
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _display.Received(1).Clear();
    }

    [Test]
    public void SetPower_DoorOpened_DisplayCleared()
    {
        // Also checks if TimeButton is subscribed
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _door.Opened += Raise.EventWith(this, EventArgs.Empty);

        _display.Received(1).Clear();
    }

    [Test]
    public void SetPower_DoorOpened_LightOn()
    {
        // Also checks if TimeButton is subscribed
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _door.Opened += Raise.EventWith(this, EventArgs.Empty);

        _light.Received(1).TurnOn();
    }

    [Test]
    public void SetPower_TimeButton_TimeIs1()
    {
        // Also checks if TimeButton is subscribed
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _display.Received(1).ShowTime(Arg.Is(1), Arg.Is(0));
    }

    [Test]
    public void SetPower_2TimeButton_TimeIs2()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _display.Received(1).ShowTime(Arg.Is(2), Arg.Is(0));
    }

    [Test]
    public void SetTime_StartButton_CookerIsCalled()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _cooker.Received(1).StartCooking(50, 60);
    }

    [Test]
    public void SetTime_DoorOpened_DisplayCleared()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _door.Opened += Raise.EventWith(this, EventArgs.Empty);

        _display.Received().Clear();
    }

    [Test]
    public void SetTime_DoorOpened_LightOn()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _door.Opened += Raise.EventWith(this, EventArgs.Empty);

        _light.Received().TurnOn();
    }

    [Test]
    public void Ready_PowerAndTime_CookerIsCalledCorrectly()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        // Should call with correct values
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _cooker.Received(1).StartCooking(100, 120);
    }

    [Test]
    public void Ready_FullPower_CookerIsCalledCorrectly()
    {
        for (var i = 50; i <= 700; i += 50)
        {
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        }

        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime

        // Should call with correct values
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _cooker.Received(1).StartCooking(700, 60);

    }


    [Test]
    public void SetTime_StartButton_LightIsCalled()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now cooking

        _light.Received(1).TurnOn();
    }

    [Test]
    public void Cooking_CookingIsDone_LightOff()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in cooking

        _uut.CookingIsDone();
        _light.Received(1).TurnOff();
    }

    [Test]
    public void Cooking_CookingIsDone_ClearDisplay()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in cooking

        // Cooking is done
        _uut.CookingIsDone();
        _display.Received(1).Clear();
    }

    [Test]
    public void Cooking_DoorIsOpened_CookerCalled()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in cooking

        // Open door
        _door.Opened += Raise.EventWith(this, EventArgs.Empty);

        _cooker.Received(1).Stop();
    }

    [Test]
    public void Cooking_DoorIsOpened_DisplayCleared()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in cooking

        // Open door
        _door.Opened += Raise.EventWith(this, EventArgs.Empty);

        _display.Received(1).Clear();
    }

    [Test]
    public void Cooking_CancelButton_CookerCalled()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in cooking

        // Open door
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _cooker.Received(1).Stop();
    }

    [Test]
    public void Cooking_CancelButton_LightCalled()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in cooking

        // Open door
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _light.Received(1).TurnOff();
    }

    #region Buzzer

    private void GoToCooking()
    {
        _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetPower
        _timeButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in SetTime
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
        // Now in Cooking
    }

    [Test]
    public void Cooking_CookingIsDone_BuzzerBuzzes3Times()
    {
        GoToCooking();

        _uut.CookingIsDone();

        _buzzer.Received(1).Buzz(3);
    }

    [Test]
    public void Cooking_CancelButton_BuzzerNotCalled()
    {
        GoToCooking();

        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _buzzer.DidNotReceiveWithAnyArgs().Buzz(default);
    }

    [Test]
    public void Cooking_DoorOpened_BuzzerNotCalled()
    {
        GoToCooking();

        _door.Opened += Raise.EventWith(this, EventArgs.Empty);

        _buzzer.DidNotReceiveWithAnyArgs().Buzz(default);
    }

    [Test]
    public void Ready_CookingIsDone_BuzzerNotCalled()
    {
        // CookingIsDone should be ignored when not cooking
        _uut.CookingIsDone();

        _buzzer.DidNotReceiveWithAnyArgs().Buzz(default);
    }

    [Test]
    public void Cooking_CookingIsDoneThenCancel_BuzzerOnlyCalledOnce()
    {
        GoToCooking();

        _uut.CookingIsDone();
        // Now in Ready - start/cancel does nothing here
        _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

        _buzzer.Received(1).Buzz(Arg.Any<int>());
    }

    #endregion
}
