using System;
using Microwave.Classes.Boundary.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        public const int MinConfigurablePower = 400;
        public const int MaxConfigurablePower = 1200;
        private readonly IOutput myOutput;
        private bool IsOn = false;

        public int MaxPower { get; }

        public PowerTube(IOutput output, int maxPower = 700)
        {
            if (maxPower < MinConfigurablePower || maxPower > MaxConfigurablePower)
            {
                throw new ArgumentOutOfRangeException("maxPower", maxPower, $"Must be between {MinConfigurablePower} and {MaxConfigurablePower} (incl.)");
            }

            myOutput = output;
            MaxPower = maxPower;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || power > MaxPower)
            {
                throw new ArgumentOutOfRangeException(nameof(power), power, $"Must be between 1 and {MaxPower} (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}