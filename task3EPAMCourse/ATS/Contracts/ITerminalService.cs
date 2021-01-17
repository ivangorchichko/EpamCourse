using System.Collections.Generic;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface ITerminalService
    {
        IEnumerable<ITerminal> Terminals { get; }

        ITerminal GetAvaibleTerminal();
    }
}
