namespace Microwave.Classes.StateMachine.MicrowaveControl.States;

public class Ready : MicrowaveControllerState
{
    public override void OnPowerPressed(IMicrowaveStateContext context)
    {
        context.ShowPower();
        context.SetState(new SetPower());
    }
    
    public override void OnDoorOpened(IMicrowaveStateContext context)
    {
        context.TurnOnLight();
        context.SetState(new DoorOpen());
    }
}