using System;
using Microwave.Classes.Boundary.Interfaces;

namespace Microwave.Classes.Boundary;

public class Buzzer : IBuzzer
{
    private readonly IOutput myOutput;

    public Buzzer(IOutput output)
    {
        myOutput = output;
    }

    public void Buzz(int bursts)
    {
        if (bursts < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(bursts), "Number of bursts must be at least 1");
        }

        for (var i = 0; i < bursts; i++)
        {
            myOutput.OutputLine("Buzzer: BZZZ");
        }
    }
}
