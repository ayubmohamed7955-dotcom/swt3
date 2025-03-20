using System;

namespace Microwave.Classes.Boundary.Interfaces;

public interface ITimeProvider
{
    int TimeRemaining { get; }
    event EventHandler Expired;
    event EventHandler TimerTick;

    void Start(int time);
    void Stop();
}