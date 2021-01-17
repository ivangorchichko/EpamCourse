using System;
using System.Collections.Generic;
using System.Linq;
using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Enums;
using Task3EPAMCourse.ATS.Model;

namespace Task3EPAMCourse.ATS.Service
{
    public class TerminalService : ITerminalService
    {
        public TerminalService(AutoTelephoneStation autoTelephoneStation)
        {
            Terminals = new List<ITerminal>()
            {
                 new Terminal("001", TerminalCondition.Available, autoTelephoneStation),
                 new Terminal("002", TerminalCondition.Available, autoTelephoneStation),
                 new Terminal("003", TerminalCondition.Available, autoTelephoneStation),
                 new Terminal("004", TerminalCondition.Available, autoTelephoneStation),
            };
        }

        public IList<ITerminal> Terminals { get; }

        public ITerminal GetAvailableTerminal()
        {
            var terminal = Terminals.FirstOrDefault(availableTerminal => availableTerminal.TerminalCondition == TerminalCondition.Available);
            if (terminal != null)
            {
                return terminal;
            }
            else
            {
                Console.WriteLine("All terminals are in used");
                return null;
            }
        }

        public void CreateNewTerminal(ITerminal newTerminal)
        {
            Terminals.Add(newTerminal);
        }
    }
}
