using System;
using System.Collections.Generic;
using System.Text;

namespace task3EPAMCourse.ATS.Contracts
{
    public interface ITerminalService
    {
        IEnumerable<ITerminal> Terminals { get; }
        ITerminal GetAvaibleTerminal();
    }
}
