using Microwave.Classes.Domain.MicrowaveControllerStateMachine.Interfaces;

namespace Microwave.Classes.Domain.MicrowaveControllerStateMachine.States;

public class DoorOpen : MicrowaveControllerState
{
    public override void OnDoorClosed(IMicrowaveStateContext context)
    {
        context.TurnOffLight();
        context.SetState(new Ready());
    }
}