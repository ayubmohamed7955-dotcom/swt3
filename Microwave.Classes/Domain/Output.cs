using Microwave.Classes.Domain.Interfaces;

namespace Microwave.Classes.Domain;

public class Output : IOutput
{
    public void OutputLine(string line)
    {
        System.Console.WriteLine(line);
    }
    
}