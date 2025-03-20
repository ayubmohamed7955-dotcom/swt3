namespace Microwave.Classes.StateMachine.MicrowaveControl.States;

public class Cooking : MicrowaveControllerState
{
    public override void OnStartCancelPressed(IMicrowaveStateContext context)
    {
        context.StopCooking();
        context.SetState(new Ready());
    }
    
    public override void OnDoorOpened(IMicrowaveStateContext context)
    {
        context.StopCookingLeaveLightOn();
        context.SetState(new DoorOpen());
    }

    public override void CookingIsDone(IMicrowaveStateContext context)
    {
        context.CompleteCooking();
        context.SetState(new Ready());
    }
}