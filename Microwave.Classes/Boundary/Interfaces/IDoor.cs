using System;

namespace Microwave.Classes.Boundary.Interfaces;

public interface IDoor
{
    event EventHandler Opened;
    event EventHandler Closed;

    void Open();
    void Close();
}