using Microwave.Classes.Boundary.Interfaces;

namespace Microwave.Classes.Boundary;

public class Light(IOutput output) : ILight
{
    private bool isOn;

    public void TurnOn()
    {
        if (isOn) return;
        output.OutputLine("Light is turned on");
        isOn = true;
    }

    public void TurnOff()
    {
        if (!isOn) return;
        output.OutputLine("Light is turned off");
        isOn = false;
    }

}