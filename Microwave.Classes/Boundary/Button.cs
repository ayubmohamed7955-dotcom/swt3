using System;
using Microwave.Classes.Boundary.Interfaces;

namespace Microwave.Classes.Boundary;

public class Button : IButton
{
    public event EventHandler Pressed;

    public void Press()
    {
        Pressed?.Invoke(this, EventArgs.Empty);
    }
}

