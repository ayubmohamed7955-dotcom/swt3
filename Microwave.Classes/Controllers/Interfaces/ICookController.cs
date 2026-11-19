namespace Microwave.Classes.Controllers.Interfaces;

public interface ICookController
{
    void StartCooking(int power, int time);
    void Stop();
    void AddTime(int seconds);
}