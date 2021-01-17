using System.Collections.Generic;

namespace Task3EPAMCourse.ATS.Contracts
{
    public interface ITerminalsService
    {
        IList<ITerminal> Terminals { get; }

        ITerminal GetAvailableTerminal();

        void CreateNewTerminal(ITerminal newTerminal);
    }
}
