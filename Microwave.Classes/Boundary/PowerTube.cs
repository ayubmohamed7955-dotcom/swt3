using System;
using Microwave.Classes.Boundary.Interfaces;

namespace Microwave.Classes.Boundary;

public class PowerTube(IOutput output) : IPowerTube
{
    private bool isOn;

    public void TurnOn(int power)
    {
        if (power is < 1 or > 700)
        {
            throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 700 (incl.)");
        }

        if (isOn)
        {
            throw new ApplicationException("PowerTube.TurnOn: is already on");
        }

        output.OutputLine($"PowerTube works with {power}");
        isOn = true;
    }

    public void TurnOff()
    {
        if (isOn)
        {
            output.OutputLine($"PowerTube turned off");
        }

        isOn = false;
    }
}