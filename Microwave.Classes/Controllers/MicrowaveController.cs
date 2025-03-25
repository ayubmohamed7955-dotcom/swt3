using System;
using Microwave.Classes.Boundary.Interfaces;
using Microwave.Classes.Controllers.Interfaces;
using Microwave.Classes.StateMachine.MicrowaveControl;
using Microwave.Classes.StateMachine.MicrowaveControl.States;

namespace Microwave.Classes.Controllers;

public class MicrowaveController : IMicrowaveController, IMicrowaveStateContext
{
    private MicrowaveControllerState state;
    private readonly ICookController myCooker;
    private readonly ILight myLight;
    private readonly IDisplay myDisplay;

    private int powerLevel = 50;
    private int time = 1;

    public MicrowaveController(
        IButton powerButton,
        IButton timeButton,
        IButton startCancelButton,
        IDoor door,
        IDisplay display,
        ILight light,
        ICookController cooker)
    {
        powerButton.Pressed += OnPowerPressed;
        timeButton.Pressed += OnTimePressed;
        startCancelButton.Pressed += OnStartCancelPressed;

        door.Closed += OnDoorClosed;
        door.Opened += OnDoorOpened;

        myCooker = cooker;
        state = new Ready();
        myLight = light;
        myDisplay = display;
    }

    private void ResetValues()
    {
        powerLevel = 50;
        time = 1;
    }

    #region State Management - Called by events
    public void OnPowerPressed(object sender, EventArgs e)
    {
        state.OnPowerPressed(this);
    }

    public void OnTimePressed(object sender, EventArgs e)
    {
        state.OnTimePressed(this);
    }

    public void OnStartCancelPressed(object sender, EventArgs e)
    {
        state.OnStartCancelPressed(this);
    }

    public void OnDoorOpened(object sender, EventArgs e)
    {
        state.OnDoorOpened(this);
    }

    public void OnDoorClosed(object sender, EventArgs e)
    {
        state.OnDoorClosed(this);
    }

    public void CookingIsDone()
    {
        state.CookingIsDone(this);
    }
    
    public void SetState(MicrowaveControllerState s)
    {
        state = s;
    }
    #endregion
    
    #region State Actions - Sets values, called by the states
    public void ShowPower()
    {
        myDisplay.ShowPower(powerLevel);
    }

    public void IncreaseAndShowPower()
    {
        powerLevel = (powerLevel >= 700 ? 50 : powerLevel+50);
        myDisplay.ShowPower(powerLevel);
    }

    public void ShowTime()
    {
        myDisplay.ShowTime(time, 0);
    }

    public void IncreaseAndShowTime()
    {
        time += 1;
        myDisplay.ShowTime(time, 0);
    }

    public void Reset()
    {
        ResetValues();
        myDisplay.Clear();
    }

    public void StartCooking()
    {
        myLight.TurnOn();
        myCooker.StartCooking(powerLevel, time*60);
    }

    public void StopCooking()
    {
        ResetValues();
        myCooker.Stop();
        myLight.TurnOff();
        myDisplay.Clear();
    }
    public void StopCookingLeaveLightOn()
    {
        ResetValues();
        myCooker.Stop();
        myDisplay.Clear();
    }
    public void TurnOnLight()
    {
        myLight.TurnOn();
    }

    public void TurnOffLight()
    {
        myLight.TurnOff();
    }
    
    public void ResetWithLightOn()
    {
        ResetValues();
        myLight.TurnOn();
        myDisplay.Clear();
    }

    public void CompleteCooking()
    {
        ResetValues();
        myDisplay.Clear();
        myLight.TurnOff();
        // Beep 3 times
    }
    #endregion
}