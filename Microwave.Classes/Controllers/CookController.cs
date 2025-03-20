using System;
using Microwave.Classes.Boundary.Interfaces;
using Microwave.Classes.Controllers.Interfaces;

namespace Microwave.Classes.Controllers;

public class CookController : ICookController
{
    // Since there is a 2-way association, this cannot be set until the UI object has been created
    // It also demonstrates property dependency injection
    public IMicrowaveController Microwave { set; private get; }

    private bool isCooking;

    private readonly IDisplay myDisplay;
    private readonly IPowerTube myPowerTube;
    private readonly ITimeProvider myTimer;

    public CookController(
        ITimeProvider timer,
        IDisplay display,
        IPowerTube powerTube,
        IMicrowaveController microwave) : this(timer, display, powerTube)
    {
        Microwave = microwave;
    }

    public CookController(
        ITimeProvider timer,
        IDisplay display,
        IPowerTube powerTube)
    {
        myTimer = timer;
        myDisplay = display;
        myPowerTube = powerTube;

        timer.Expired += OnTimerExpired;
        timer.TimerTick += OnTimerTick;
    }

    public void StartCooking(int power, int time)
    {
        myPowerTube.TurnOn(power);
        myTimer.Start(time);
        isCooking = true;
    }

    public void Stop()
    {
        isCooking = false;
        myPowerTube.TurnOff();
        myTimer.Stop();
    }

    private void OnTimerExpired(object sender, EventArgs e)
    {
        if (!isCooking) return;
        isCooking = false;
        myPowerTube.TurnOff();
        Microwave.CookingIsDone();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        if (!isCooking) return;
        var remaining = myTimer.TimeRemaining;
        myDisplay.ShowTime(remaining / 60, remaining % 60);
    }
}