namespace Microwave.Classes.Domain.MicrowaveControllerStateMachine.Interfaces;

public interface IMicrowaveStateContext
{
    void ShowPower();
    void IncreaseAndShowPower();
    void ShowTime();
    void IncreaseAndShowTime();
    void Reset();
    void StartCooking();
    void StopCooking();
    void StopCookingLeaveLightOn();
    void TurnOnLight();
    void TurnOffLight();
    void ResetWithLightOn();
    void CompleteCooking();
    void SetState(MicrowaveControllerState s);
}