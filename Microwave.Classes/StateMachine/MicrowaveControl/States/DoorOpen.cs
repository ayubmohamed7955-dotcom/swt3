namespace Microwave.Classes.StateMachine.MicrowaveControl.States;

public class DoorOpen : MicrowaveControllerState
{
    public override void OnDoorClosed(IMicrowaveStateContext context)
    {
        context.TurnOffLight();
        context.SetState(new Ready());
    }
}