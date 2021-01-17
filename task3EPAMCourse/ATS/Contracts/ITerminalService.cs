using System.Collections.Generic;

namespace Task3EPAMCourse.ATS.Contracts
{
    public interface ITerminalService
    {
        IEnumerable<ITerminal> Terminals { get; }

        ITerminal GetAvailableTerminal();
    }
}
