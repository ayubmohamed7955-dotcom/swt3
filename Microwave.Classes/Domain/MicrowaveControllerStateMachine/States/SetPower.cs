using Microwave.Classes.Domain.MicrowaveControllerStateMachine.Interfaces;

namespace Microwave.Classes.Domain.MicrowaveControllerStateMachine.States;

public class SetPower : MicrowaveControllerState
{
    public override void OnPowerPressed(IMicrowaveStateContext context)
    {
        context.IncreaseAndShowPower();
    }

    public override void OnTimePressed(IMicrowaveStateContext context)
    {
        context.ShowTime();
        context.SetState(new SetTime());
    }

    public override void OnStartCancelPressed(IMicrowaveStateContext context)
    {
        context.Reset();
        context.SetState(new Ready());
    }
    public override void OnDoorOpened(IMicrowaveStateContext context)
    {
        context.ResetWithLightOn();
        context.SetState(new DoorOpen());
    }
}