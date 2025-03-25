using System;

namespace Microwave.Classes.Domain.Interfaces;

public interface ITimeProvider
{
    int TimeRemaining { get; }
    event EventHandler Expired;
    event EventHandler TimerTick;

    void Start(int time);
    void Stop();
}