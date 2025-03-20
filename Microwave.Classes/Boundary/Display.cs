using Microwave.Classes.Boundary.Interfaces;

namespace Microwave.Classes.Boundary;

public class Display(IOutput output) : IDisplay
{
    public void ShowTime(int min, int sec)
    {
        output.OutputLine($"Display shows: {min:D2}:{sec:D2}");
    }

    public void ShowPower(int power)
    {
        output.OutputLine($"Display shows: {power} W");
    }

    public void Clear()
    {
        output.OutputLine($"Display cleared");
    }
}
