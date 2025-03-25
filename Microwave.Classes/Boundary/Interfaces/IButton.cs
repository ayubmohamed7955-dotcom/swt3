using System;

namespace Microwave.Classes.Boundary.Interfaces;
public interface IButton
{
    event EventHandler Pressed;

    void Press();
}
