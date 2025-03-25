namespace Microwave.Classes.StateMachine.MicrowaveControl;

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