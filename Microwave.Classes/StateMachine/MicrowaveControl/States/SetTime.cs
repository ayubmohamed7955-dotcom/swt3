namespace Microwave.Classes.StateMachine.MicrowaveControl.States;

public class SetTime : MicrowaveControllerState
{
    public override void OnTimePressed(IMicrowaveStateContext context)
    {
        context.IncreaseAndShowTime();
    }
    
    public override void OnStartCancelPressed(IMicrowaveStateContext context)
    {
        context.StartCooking();
        context.SetState(new Cooking());
    }
    
    public override void OnDoorOpened(IMicrowaveStateContext context)
    {
        context.ResetWithLightOn();
        context.SetState(new DoorOpen());
    }
}