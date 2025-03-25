using System;

namespace Microwave.Classes.Controllers.Interfaces;

public interface IMicrowaveController
{
    void OnPowerPressed(object sender, EventArgs e);
    void OnTimePressed(object sender, EventArgs e);
    void OnStartCancelPressed(object sender, EventArgs e);

    void OnDoorOpened(object sender, EventArgs e);
    void OnDoorClosed(object sender, EventArgs e);

    void CookingIsDone();
}