namespace Microwave.Classes.Boundary.Interfaces;

public interface IBuzzer
{
    /// <summary>
    /// Sends out a number of short bursts of buzzing sound.
    /// </summary>
    /// <param name="bursts">Number of bursts. Must be at least 1.</param>
    void Buzz(int bursts);
}
