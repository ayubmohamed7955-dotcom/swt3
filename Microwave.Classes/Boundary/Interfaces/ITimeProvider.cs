using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microwave.Classes.Boundary.Interfaces;

public interface ITimeProvider
{
    int TimeRemaining { get; }
    event EventHandler Expired;
    event EventHandler TimerTick;

    void Start(int time);
    void Stop();
}
