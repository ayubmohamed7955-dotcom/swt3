namespace Microwave.Classes.StateMachine.MicrowaveControl;

public abstract class MicrowaveControllerState
{
    public virtual void OnPowerPressed(IMicrowaveStateContext context) { }
    public virtual void OnTimePressed(IMicrowaveStateContext context) { }
    public virtual void OnStartCancelPressed(IMicrowaveStateContext context) { }
    public virtual void OnDoorClosed(IMicrowaveStateContext context) { }
    public virtual void OnDoorOpened(IMicrowaveStateContext context) { }
    public virtual void CookingIsDone(IMicrowaveStateContext context) { }
}